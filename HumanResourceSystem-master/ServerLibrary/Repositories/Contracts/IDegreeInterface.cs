
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IDegreeInterface
    {
        Task<List<Degree>> GetAll();
        Task<Degree> GetById(int trainingProgramId, int employeeId);
        Task<List<Degree>> GetAllByTrainingProgramId(int trainingProgramId);
        Task<List<Degree>> GetAllByEmployeeId(int employeeId);
        Task<GeneralResponse> Insert(Degree item);
        Task<GeneralResponse> DeleteById(int id);
        Task<GeneralResponse> Update(Degree item);
    }
}
