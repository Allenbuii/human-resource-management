
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class CandidateService : GenericServiceImplementation<Candidate>, ICandidateService
    {
        private readonly GetHttpClient _getHttpClient;

        public CandidateService(GetHttpClient getHttpClient) : base(getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }
        public async Task<GeneralResponse> SendEmail(Candidate candidate, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/send-email", candidate);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }
}
