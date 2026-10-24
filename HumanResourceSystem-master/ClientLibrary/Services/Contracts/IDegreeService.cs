
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IDegreeService
    {
        Task<List<Degree>> GetAll(string baseUrl);
        Task<Degree> GetById(int trainingProgramId, int employeeId, string baseUrl);
        Task<List<Degree>> GetAllEmployee(int trainingProgramId, string baseUrl);
        Task<List<Degree>> GetAllTrainingProgram(int employeeId, string baseUrl);
        Task<GeneralResponse> Insert(Degree item, string baseUrl);
        Task<GeneralResponse> DeleteById(int id, string baseUrl);
        Task<GeneralResponse> Update(Degree item, string baseUrl);

    }
}
