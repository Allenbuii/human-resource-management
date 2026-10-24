
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class PeriodicEvaluationService : GenericServiceImplementation<PeriodicEvaluation>, IPeriodicEvaluationService
    {
        private readonly GetHttpClient _getHttpClient;

        public PeriodicEvaluationService(GetHttpClient getHttpClient) : base(getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }
        public async Task<List<PeriodicEvaluation>> GetAllByEmployeeId(int employeeid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<PeriodicEvaluation>>($"{baseUrl}/all/employee/{employeeid}");
            return results!;
        }

        public async Task<PeriodicEvaluation> GetClosestByEmployeeId(int employeeid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<PeriodicEvaluation>($"{baseUrl}/closest/{employeeid}");
            return result!;
        }

        public async Task<GeneralResponse> SendEmail(string message, PeriodicEvaluation item, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/send-email/{message}", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
