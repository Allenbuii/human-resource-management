
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Employee>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Employees.FindAsync(id);
            if (item is null) return NotFound();
            int BranchId = item.BranchId;
            appDbContext.Employees.Remove(item);            

            await UpdateEmployeeCount(BranchId, -1);

            await Commit();
            return Success();
        }

        public async Task<List<Employee>> GetAll()
        {
            var employees = await appDbContext.Employees
            .AsNoTracking()
            .Include(t => t.Town)
            .ThenInclude(b => b.City)
            .ThenInclude(c => c.Country)
            .Include(b => b.Branch)
            .ThenInclude(d => d.Department)
            .ThenInclude(gd => gd.GeneralDepartment)
            .Include(u => u.ApplicationUser)
            .ToListAsync();

            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await appDbContext.Employees
            .Include(t => t.Town)
            .ThenInclude(b => b.City)
            .ThenInclude(c => c.Country)
            .Include(b => b.Branch)
            .ThenInclude(d => d.Department)
            .ThenInclude(gd => gd.GeneralDepartment)
            .Include(u => u.ApplicationUser)
            .FirstOrDefaultAsync(ei => ei.Id == id);

            return employee!;
        }

        public async Task<GeneralResponse> Insert(Employee item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, $"{item.Name} already added");
            appDbContext.Employees.Add(item);

            await UpdateEmployeeCount(item.BranchId, 1);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Employee employee)
        {
            var findUser = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if (findUser is null) return new GeneralResponse(false, "Employee does not exist");

            int OldBranchId = findUser.BranchId;

            findUser.Name = employee.Name;
            findUser.Email = employee.Email;
            findUser.Gender = employee.Gender;
            findUser.DateOfBirth = employee.DateOfBirth;
            findUser.CivilId = employee.CivilId;
            findUser.FileNumber = employee.FileNumber;
            findUser.JobName = employee.JobName;
            findUser.Salary = employee.Salary;
            findUser.Address = employee.Address;
            findUser.TelephoneNumber = employee.TelephoneNumber;
            findUser.Photo = employee.Photo;
            findUser.Other = employee.Other;

            findUser.BranchId = employee.BranchId;
            findUser.Branch = employee.Branch;
            findUser.TownId = employee.TownId;
            findUser.Town = employee.Town;           

            if (OldBranchId != employee.BranchId)
            {
                await UpdateEmployeeCount(OldBranchId, -1);
                await UpdateEmployeeCount(employee.BranchId, 1);
            }

            await Commit();
            return Success();
        }

        public async Task UpdateEmployeeCount(int branchId, int Count)
        {
            var branch = await appDbContext.Branches.FindAsync(branchId);
            if (branch != null)
            {
                branch.CurrentEmployees += Count;
            }
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry employee not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
