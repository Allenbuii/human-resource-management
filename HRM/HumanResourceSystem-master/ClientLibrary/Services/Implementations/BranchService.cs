
using BaseLibrary.Entities;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class BranchService : GenericServiceImplementation<Branch>, IBranchService
    {
        private readonly GetHttpClient _getHttpClient;

        public BranchService(GetHttpClient getHttpClient) : base(getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }
        public async Task<List<Employee>> GetAllEmployeeInBranch(int branchid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Employee>>($"{baseUrl}/all/employee/{branchid}");
            return results!;
        }

        public async Task<List<Employee>> GetAllNonLeader(int branchid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Employee>>($"{baseUrl}/all/leader/{branchid}");
            return results!;
        }
    }
}
