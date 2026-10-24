
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IBonusInterface : IGenericRepositoryInterface<Bonus>
    {
        Task<List<Bonus>> GetAllByEmployeeId(int employeeId);
    }
}
