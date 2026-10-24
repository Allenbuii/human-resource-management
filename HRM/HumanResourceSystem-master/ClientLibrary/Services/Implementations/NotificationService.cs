using BaseLibrary.Entities;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class NotificationService(GetHttpClient getHttpClient) : INotificationService
    {
        public async Task<List<Notification>> GetByEmployeeId(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<Notification>>($"{baseUrl}/all/employee/{id}");
            return result!;
        }

        public async Task MarkAsRead(int id, string baseUrl)    
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            await httpClient.PutAsync($"{baseUrl}/markasread/employee/{id}", null);
        }
    }
}
