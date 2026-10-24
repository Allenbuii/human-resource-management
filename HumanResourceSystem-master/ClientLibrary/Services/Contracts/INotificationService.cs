
using BaseLibrary.Entities;

namespace ClientLibrary.Services.Contracts
{
    public interface INotificationService
    {
        Task<List<Notification>> GetByEmployeeId(int id, string baseUrl);
        Task MarkAsRead(int id, string baseUrl);
    }
}
