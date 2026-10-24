
using BaseLibrary.Entities;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class EmployeeTransferService : GenericServiceImplementation<EmployeeTransfer>, IEmployeeTransferService
    {
        private readonly GetHttpClient _getHttpClient;

        public EmployeeTransferService(GetHttpClient getHttpClient) : base(getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }
        public async Task<EmployeeTransfer> GetByEmployeeId(int employeeid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<EmployeeTransfer>($"{baseUrl}/employee/{employeeid}");
            return results!;
        }

        public async Task<List<EmployeeTransfer>> GetAllByBranchId(int branchid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<EmployeeTransfer>>($"{baseUrl}/all/branch/{branchid}");
            return results!;
        }

        public async Task<List<Branch>> GetBranchesWithStaffShortage(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Branch>>($"{baseUrl}/all/branches/shortage");
            return results!;
        }

        public async Task<List<Branch>> GetBranchesWithExcssStaff(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Branch>>($"{baseUrl}/all/branches/excess");
            return results!;
        }

        public async Task<List<Branch>> GetBranchesWithStaffFully(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Branch>>($"{baseUrl}/all/branches/fully");
            return results!;
        }

        public async Task<List<Employee>> GetAllExcssEmployee(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Employee>>($"{baseUrl}/all/employees/excess");
            return results!;
        }
    }
}
