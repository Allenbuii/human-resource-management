
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class NotificationRepository(AppDbContext appDbContext) : INotificationInterface
    {
        public async Task<List<Notification>> GetByEmployeeId(int id)
        {
            var notifications = await appDbContext.Notifications
                .AsNoTracking()
                .Where(u => u.EmployeeId == id)
                .ToListAsync();

            return notifications!;
        }

        public async Task MarkAsRead(int id)
        {
            var notifications = await appDbContext.Notifications
                .Where(n => n.EmployeeId == id && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await appDbContext.SaveChangesAsync();
        }
    }
}
