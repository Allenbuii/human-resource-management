
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeTransferRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<EmployeeTransfer>, IEmployeeTransferInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.EmployeeTransfers.FindAsync(id);
            if (item is null) return NotFound();
            int? BranchId = item.BranchId;
            appDbContext.EmployeeTransfers.Remove(item);

            await UpdateEmployeeTransfersCount(BranchId, -1);

            await Commit();
            return Success();
        }

        public async Task<List<EmployeeTransfer>> GetAll()
        {
            var employeeTransfers = await appDbContext.EmployeeTransfers
                .AsNoTracking()
                .Include(e => e.Employee)
                .ThenInclude(eb => eb.Branch)
                .Include(b => b.Branch)
                .ToListAsync();

            return employeeTransfers!;
        }

        public async Task<EmployeeTransfer> GetByEmployeeId(int employeeid)
        {
            var employeeTransfer = await appDbContext.EmployeeTransfers
                .Include(e => e.Employee)
                .Include(b => b.Branch)
                .ThenInclude(d => d.Department)
                .ThenInclude(gd => gd.GeneralDepartment)
                .FirstOrDefaultAsync(et => et.EmployeeId == employeeid);

            return employeeTransfer!;
        }

        public async Task<List<EmployeeTransfer>> GetAllByBranchId(int branchid)
        {
            var employeeTransfers = await appDbContext.EmployeeTransfers
                .AsNoTracking()
                .Where(p => p.BranchId == branchid)
                .Include(e => e.Employee)
                .ThenInclude(b => b.Branch)
                .ThenInclude(d => d.Department)
                .ThenInclude(gd => gd.GeneralDepartment)
                .Include(b => b.Branch)
                .ToListAsync();

            return employeeTransfers!;
        }

        public async Task<EmployeeTransfer> GetById(int id) => await appDbContext.EmployeeTransfers.FindAsync(id);

        public async Task<GeneralResponse> Insert(EmployeeTransfer item)
        {
            appDbContext.EmployeeTransfers.Add(item);

            string message = $"You have been transferred to a new position. See more information in your profile.";
            await AddNotification((int)item.EmployeeId!, message);

            await UpdateEmployeeTransfersCount(item.BranchId, 1);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(EmployeeTransfer item)
        {
            var employeeTransfers = await appDbContext.EmployeeTransfers.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (employeeTransfers is null) return new GeneralResponse(false, "Employee Transfer does not exist");

            int? OldBranchId = employeeTransfers.BranchId;

            employeeTransfers.Name = item.Name;
            employeeTransfers.NewPosition = item.NewPosition;
            employeeTransfers.RequestDate = item.RequestDate;
            employeeTransfers.Reason = item.Reason;

            employeeTransfers.EmployeeRequestName = item.EmployeeRequestName;
            employeeTransfers.EmployeeRequest = item.EmployeeRequest;

            employeeTransfers.EmployeeId = item.EmployeeId;
            employeeTransfers.Employee = item.Employee;

            employeeTransfers.BranchId = item.BranchId;
            employeeTransfers.Branch = item.Branch;

            if (OldBranchId != item.BranchId)
            {
                await UpdateEmployeeTransfersCount(OldBranchId, -1);
                await UpdateEmployeeTransfersCount(item.BranchId, 1);
            }

            await Commit();
            return Success();
        }

        public async Task<List<Branch>> GetBranchesWithStaffShortage()
        {
            var branch = await appDbContext.Branches
                .AsNoTracking()
                .Where(b => b.CurrentEmployees + b.CurrentEmployeeTransfers < b.RequiredEmployees)
                .Include(d => d.Department)
                .ToListAsync();

            return branch!;
        }

        public async Task<List<Branch>> GetBranchesWithExcssStaff()
        {
            var branch = await appDbContext.Branches
                .AsNoTracking()
                .Where(b => b.CurrentEmployees > b.RequiredEmployees)
                .Include(d => d.Department)
                .ToListAsync();

            return branch!;
        }

        public async Task<List<Branch>> GetBranchesWithStaffFully()
        {
            var branch = await appDbContext.Branches
                .AsNoTracking()
                .Where(b => b.CurrentEmployees + b.CurrentEmployeeTransfers == b.RequiredEmployees && b.CurrentEmployeeTransfers > 0)
                .Include(d => d.Department)
                .ToListAsync();

            return branch!;
        }

        public async Task UpdateEmployeeTransfersCount(int? branchId, int Count)
        {
            var branch = await appDbContext.Branches.FindAsync(branchId);
            if (branch != null)
            {
                branch.CurrentEmployeeTransfers += Count;
            }
        }

        public async Task<List<Employee>> GetAllExcssEmployee()
        {
            var excessBranches = await GetBranchesWithExcssStaff();

            var employees = await appDbContext.Employees
                .AsNoTracking()
                .Where(e => excessBranches.Select(b => b.Id).Contains(e.BranchId))
                .Include(b => b.Branch)
                .ThenInclude(d => d.Department)
                .ThenInclude(gd => gd.GeneralDepartment)
                .ToListAsync();

            return employees!;
        }

        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                Type = "Employee Transfer",
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            appDbContext.Notifications.Add(notification);
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry Employee Transfer not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
