using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IPeriodicEvaluationInterface : IGenericRepositoryInterface<PeriodicEvaluation>
    {
        Task<List<PeriodicEvaluation>> GetAllByEmployeeId(int employeeid);
        Task<PeriodicEvaluation> GetClosestByEmployeeId(int employeeid);
        Task<GeneralResponse> SendEmail(string messageType, PeriodicEvaluation candidate);
    }
}
