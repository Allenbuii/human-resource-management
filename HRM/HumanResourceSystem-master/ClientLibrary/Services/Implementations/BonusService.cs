
using BaseLibrary.Entities;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class BonusService : GenericServiceImplementation<Bonus>, IBonusService
    {
        private readonly GetHttpClient _getHttpClient;

        public BonusService(GetHttpClient getHttpClient) : base(getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }
        public async Task<List<Bonus>> GetAllByEmployeeId(int employeeid, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<Bonus>>($"{baseUrl}/all/employee/{employeeid}");
            return results!;
        }
    }
}
