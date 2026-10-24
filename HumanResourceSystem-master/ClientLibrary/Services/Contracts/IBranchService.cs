using BaseLibrary.Entities;

namespace ClientLibrary.Services.Contracts
{
    public interface IBranchService : IGenericServiceInterface<Branch>
    {
        Task<List<Employee>> GetAllEmployeeInBranch(int branchid, string baseUrl);
        Task<List<Employee>> GetAllNonLeader(int branchid, string baseUrl);
    }
}
