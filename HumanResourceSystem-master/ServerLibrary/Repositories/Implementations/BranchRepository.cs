
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class BranchRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Branch>, IBranchInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Branches.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Branches.Remove(dep);

            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetAll() => await appDbContext
            .Branches
            .AsNoTracking()
            .Include(d => d.Department)
            .ToListAsync();
        public async Task<Branch> GetById(int id) => await appDbContext.Branches.FindAsync(id);

        public async Task<GeneralResponse> Insert(Branch item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, $"{item.Name} already added");
            appDbContext.Branches.Add(item);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Branch item)
        {
            var branch = await appDbContext.Branches.FindAsync(item.Id);
            if (branch is null) return NotFound();
            branch.Name = item.Name;
            branch.SupervisorId = item.SupervisorId;
            branch.SupervisorName = item.SupervisorName;
            branch.TelephoneNumber = item.TelephoneNumber;
            branch.Email = item.Email;
            branch.RequiredEmployees = item.RequiredEmployees;

            branch.DepartmentId = item.DepartmentId;
            branch.Department = item.Department;

            await Commit();
            return Success();
        }

        public async Task<List<Employee>> GetAllEmployeeInBranch(int branchId)
        {
            var employees = await appDbContext.Employees
            .AsNoTracking()
            .Where(e => e.BranchId == branchId)
            .ToListAsync();

            return employees;
        }

        public async Task<List<Employee>> GetAllNonLeader(int branchId)
        {
            var allLeaders = new List<int>();

            var GeneralDepartmentLeaders = await appDbContext.GeneralDepartments
            .Where(gd => gd.DirectorId != 0)
            .Select(gd => gd.DirectorId)
            .ToListAsync();

            var DepartmentLeaders = await appDbContext.Departments
            .Where(gd => gd.ManagerId != 0)
            .Select(gd => gd.ManagerId)
            .ToListAsync();

            var BrancheLeaders = await appDbContext.Branches
            .Where(gd => gd.SupervisorId != 0)
            .Select(gd => gd.SupervisorId)
            .ToListAsync();

            allLeaders.AddRange(GeneralDepartmentLeaders);
            allLeaders.AddRange(DepartmentLeaders);
            allLeaders.AddRange(BrancheLeaders);

            var employees = await appDbContext.Employees
            .AsNoTracking()
            .Where(e => e.BranchId == branchId && !allLeaders.Contains(e.Id)) 
            .ToListAsync();

            return employees;
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry branch not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Branches.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
