using BaseLibrary.Entities;

namespace ClientLibrary.Services.Contracts
{
    public interface IBonusService : IGenericServiceInterface<Bonus>
    {
        Task<List<Bonus>> GetAllByEmployeeId(int employeeid, string baseUrl);
    }
}
