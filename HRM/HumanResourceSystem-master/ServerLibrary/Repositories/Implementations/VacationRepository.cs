using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class VacationRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Vacation>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Vacations.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Vacations.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Vacation>> GetAll() => await appDbContext
            .Vacations
            .AsNoTracking()
            .Include(t => t.VacationType)
            .Include(e => e.Employee)
            .ToListAsync();

        public async Task<Vacation> GetById(int id) => await appDbContext.Vacations.FindAsync(id);

        public async Task<GeneralResponse> Insert(Vacation item)
        {
            appDbContext.Vacations.Add(item);

            if (item.Status == "Accept" || item.Status == "Reject")
            {
                string message = "You have a new vacation request added by your supervisor.";
                await AddNotification(item.EmployeeId, message);
            }

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Vacation item)
        {
            var obj = await appDbContext.Vacations.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.StartDate = item.StartDate;
            obj.NumberOfDays = item.NumberOfDays;
            obj.Status = item.Status;
            obj.Description = item.Description;
            obj.VacationTypeId = item.VacationTypeId;
            obj.VacationType = item.VacationType;

            if (item.Status == "Accept" || item.Status == "Reject")
            {
                string message = $"Your vacation request has been {item.Status.ToLower()}.";
                await AddNotification(item.EmployeeId, message);
            }

            await Commit();
            return Success();
        }

        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                Type = "Vacation",
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            appDbContext.Notifications.Add(notification);
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry data not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
