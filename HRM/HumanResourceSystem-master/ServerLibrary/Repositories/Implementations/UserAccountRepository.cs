using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "Model is empty");

            var checkUser = await FindUserByEmail(user.Email!);
            if (checkUser != null) return new GeneralResponse(false, "User registered already");

            var applicationUser = await AddToDataBase(new ApplicationUser()
            {
                Fullname = user.Fullname,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                EmployeeId = user.EmployeeId,
            });

            //check, create and assign role
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDataBase(new SystemRole() { Name = Constants.Admin });
                await AddToDataBase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account created!");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Staff));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDataBase(new SystemRole() { Name = Constants.Staff });
                await AddToDataBase(new UserRole() { RoleId = response.Id, UserId = applicationUser.Id });
            }
            else
            {
                await AddToDataBase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });
            }

            string message = $"Welcome aboard, {user.Fullname}! Your account has been successfully created.";
            await AddNotification(user.EmployeeId, message);

            return new GeneralResponse(true, "Account created!");

        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null) return new LoginResponse(false, "Model is empty");

            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null) return new LoginResponse(false, "User not found");

            //Check locked account
            if (applicationUser.LockoutEndTime.HasValue && applicationUser.LockoutEndTime > DateTime.Now)
            {
                var timeRemaining = applicationUser.LockoutEndTime.Value - DateTime.Now;
                var minutes = (int)timeRemaining.TotalMinutes; // Tổng số phút
                var seconds = timeRemaining.Seconds; // Số giây còn lại

                return new LoginResponse(false, $"Account is locked. Try again after {minutes} minutes and {seconds} seconds.");
            }

            //Verity password
            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
            {
                if (!applicationUser.FailedLoginStart.HasValue || applicationUser.FailedLoginStart <= DateTime.Now.AddHours(-1))
                {
                    applicationUser.FailedLoginAttempts = 0;
                    applicationUser.FailedLoginStart = DateTime.Now;
                }
                applicationUser.FailedLoginAttempts++;

                if (applicationUser.FailedLoginAttempts >= 5)
                {
                    applicationUser.LockoutEndTime = DateTime.Now.AddMinutes(30);
                    applicationUser.FailedLoginAttempts = 0;
                    await appDbContext.SaveChangesAsync();
                    return new LoginResponse(false, "Account locked due to too many failed login attempts.");
                }

                await appDbContext.SaveChangesAsync();
                return new LoginResponse(false, "Email/Password not valid");
            }

            // Reset failed login attempts on successful login
            applicationUser.FailedLoginAttempts = 0;
            applicationUser.LockoutEndTime = null;
            applicationUser.FailedLoginStart = null;

            var getUserRole = await FindUserRole(applicationUser.Id);
            if (getUserRole is null) return new LoginResponse(false, "user role not found");

            var getRoleName = await FindRoleName(getUserRole.RoleId);
            if (getRoleName is null) return new LoginResponse(false, "user role not found");

            string jwtToken = GenerateToken(applicationUser, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();

            //Save the Refresh token to the database
            var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == applicationUser.Id);
            if (findUser is not null)
            {
                findUser!.Token = refreshToken;
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                await AddToDataBase(new RefreshTokenInfo() { Token = refreshToken, UserId = applicationUser.Id });
            }
            return new LoginResponse(true, "Login successfully", jwtToken, refreshToken);
        } 

        private string GenerateToken(ApplicationUser user, string role)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Fullname!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role!)
            };
            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private async Task<UserRole> FindUserRole(int userId) => await appDbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == userId);
        private async Task<SystemRole> FindRoleName(int roleId) => await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Id == roleId);
        private async Task<ApplicationUser> FindUserByEmail(string email) => 
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<T> AddToDataBase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "Model is empty");

            var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
            if (findToken is null) return new LoginResponse(false, "Refresh token is requierd");

            //get user details
            var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Id == findToken.UserId);
            if (user is null) return new LoginResponse(false, "Refresh token could not be generated because user not found");

            var userRole = await FindUserRole(user.Id);
            var roleName = await FindRoleName(userRole.RoleId);
            string jwtToken = GenerateToken(user, roleName.Name!);
            string refershToken = GenerateRefreshToken();

            var updateRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == user.Id);
            if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");

            updateRefreshToken.Token = refershToken;
            await appDbContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed successfully", jwtToken, refershToken);
        }

        public async Task<GeneralResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (request is null) return new GeneralResponse(false, "Model is empty");

            var applicationUser = await appDbContext.ApplicationUsers.FindAsync(request.Id);
            if (applicationUser is null) return new GeneralResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, applicationUser.Password))
                return new GeneralResponse(false, "Current password is incorrect");

            applicationUser.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            appDbContext.ApplicationUsers.Update(applicationUser);
            await appDbContext.SaveChangesAsync();

            return new GeneralResponse(true, "Password changed successfully");
        }


        public async Task<List<ManageUser>> GetUsers()
        {
            var allUsers = await GetApplicationUsers();
            var allUserRoles = await UserRoles();
            var allRoles = await SystemRoles();

            if (allUsers.Count == 0 || allRoles.Count == 0) return null!;

            var users = new List<ManageUser>();
            foreach (var user in allUsers)
            {
                var userRole = allUserRoles.FirstOrDefault(u => u.UserId == user.Id);
                var roleName = allRoles.FirstOrDefault(r => r.Id == userRole!.RoleId);
                users.Add(new ManageUser() { UserId = user.Id, Name = user.Fullname!, Email = user.Email!, Role = roleName!.Name! });
            }
            return users;
        }

        public async Task<ApplicationUser> GetUserById(int id)
        {
            var user = await appDbContext.ApplicationUsers
            .FindAsync(id);

            return user!;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
            var getRole = (await SystemRoles()).FirstOrDefault(r => r.Name!.Equals(user.Role));
            var userRole = await appDbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            userRole!.RoleId = getRole!.Id;
            await appDbContext.SaveChangesAsync();
            return new GeneralResponse(true, "User role updated successfully");
        }

        public async Task<List<SystemRole>> GetRoles() => await SystemRoles();

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var user = await appDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            appDbContext.ApplicationUsers.Remove(user!);
            await appDbContext.SaveChangesAsync();
            return new GeneralResponse(true, "User successfully deleted");
        }

        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            appDbContext.Notifications.Add(notification);
        }

        private async Task<List<SystemRole>> SystemRoles() => await appDbContext.SystemRoles.AsNoTracking().ToListAsync();
        private async Task<List<UserRole>> UserRoles() => await appDbContext.UserRoles.AsNoTracking().ToListAsync();
        private async Task<List<ApplicationUser>> GetApplicationUsers() => await appDbContext.ApplicationUsers.AsNoTracking().ToListAsync();

    }
}
