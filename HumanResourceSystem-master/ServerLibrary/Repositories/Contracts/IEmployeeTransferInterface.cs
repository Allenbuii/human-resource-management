
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IEmployeeTransferInterface : IGenericRepositoryInterface<EmployeeTransfer>
    {
        Task<EmployeeTransfer> GetByEmployeeId(int employeeId);
        Task<List<EmployeeTransfer>> GetAllByBranchId(int branchid);
        Task<List<Branch>> GetBranchesWithStaffShortage();
        Task<List<Branch>> GetBranchesWithExcssStaff();
        Task<List<Branch>> GetBranchesWithStaffFully();
        Task<List<Employee>> GetAllExcssEmployee();
    }
}
