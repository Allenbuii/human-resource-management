
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        // General Department / Department / Branch
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }

        // Country / City / Town
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }

        // Authentication / Role / System Roles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        // Other / Vacation / Sanction / Doctor / Overtime
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationsTypes { get; set; }

        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }

        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }

        // Doctor
        public DbSet<Doctor> Doctors { get; set; }

        // Recruitment / Candidate
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        // TrainingProgram / Degree / Instructor
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        //PeriodicEvaluation
        public DbSet<PeriodicEvaluation> PeriodicEvaluations { get; set; }

        //Bonus
        public DbSet<Bonus> Bonuses { get; set; }

        //EmployeeTransfer
        public DbSet<EmployeeTransfer> EmployeeTransfers { get; set; }

        //Notification
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(local); Database=DemoEmployeeDb; Trusted_Connection=True; Trust Server Certificate=True;",
                sqlServerOptions => sqlServerOptions.CommandTimeout(120) 
            );
        }

    }
}
