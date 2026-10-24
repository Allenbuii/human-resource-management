
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface INotificationInterface
    {
        Task<List<Notification>> GetByEmployeeId(int id);
        Task MarkAsRead(int id);
    }
}
