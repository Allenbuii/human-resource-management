
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class DegreeService(GetHttpClient getHttpClient) : IDegreeService
    {
        // Create
        public async Task<GeneralResponse> Insert(Degree item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        // Read All
        public async Task<List<Degree>> GetAll(string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Degree>>($"{baseUrl}/all");
            return results!;
        }

        // Read All Employee By TrainingProgramId
        public async Task<List<Degree>> GetAllEmployee(int trainingProgramId, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Degree>>($"{baseUrl}/all/employee/{trainingProgramId}");
            return results!;
        }

        // Read All Training Program By EmployeeId
        public async Task<List<Degree>> GetAllTrainingProgram(int employeeId, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Degree>>($"{baseUrl}/all/trainingprogram/{employeeId}");
            return results!;
        }

        // Read Single {id}
        public async Task<Degree> GetById(int trainingProgramId, int employeeId, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<Degree>($"{baseUrl}/single/{trainingProgramId}&{employeeId}");
            return result!;
        }

        // Delete {id}
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        // Update {model}
        public async Task<GeneralResponse> Update(Degree item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
