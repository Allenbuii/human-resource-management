using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IPeriodicEvaluationService : IGenericServiceInterface<PeriodicEvaluation>
    {
        Task<List<PeriodicEvaluation>> GetAllByEmployeeId(int employeeid, string baseUrl);
        Task<PeriodicEvaluation> GetClosestByEmployeeId(int employeeid, string baseUrl);
        Task<GeneralResponse> SendEmail(string message ,PeriodicEvaluation candidate, string baseUrl);
    }
}
