
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IBranchInterface : IGenericRepositoryInterface<Branch>
    {
        Task<List<Employee>> GetAllEmployeeInBranch(int branchId);
        Task<List<Employee>> GetAllNonLeader(int branchId);
    }
}
