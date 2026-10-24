using BaseLibrary.Entities;

namespace ClientLibrary.Services.Contracts
{
    public interface IEmployeeTransferService : IGenericServiceInterface<EmployeeTransfer>
    {
        Task<EmployeeTransfer> GetByEmployeeId(int employeeid, string baseUrl);
        Task<List<EmployeeTransfer>> GetAllByBranchId(int branchid, string baseUrl);
        Task<List<Branch>> GetBranchesWithStaffShortage(string baseUrl);
        Task<List<Branch>> GetBranchesWithExcssStaff(string baseUrl);
        Task<List<Branch>> GetBranchesWithStaffFully(string baseUrl);
        Task<List<Employee>> GetAllExcssEmployee(string baseUrl);

    }
}
