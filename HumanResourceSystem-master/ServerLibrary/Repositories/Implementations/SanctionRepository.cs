using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class SanctionRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Sanction>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Sanctions.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Sanctions.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Sanction>> GetAll() => await appDbContext
            .Sanctions
            .AsNoTracking()
            .Include(t => t.SanctionType)
            .Include(e => e.Employee)
            .ToListAsync();

        public async Task<Sanction> GetById(int id) => await appDbContext.Sanctions.FindAsync(id);

        public async Task<GeneralResponse> Insert(Sanction item)
        {
            appDbContext.Sanctions.Add(item);

            string message = "You have a new sanction request added by your supervisor.";
            await AddNotification(item.EmployeeId, message);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Sanction item)
        {
            var obj = await appDbContext.Sanctions.FindAsync(item.Id);
            if (obj is null) return NotFound();
            obj.PunishmentDate = item.PunishmentDate;
            obj.Punishment = item.Punishment;
            obj.Date = item.Date;
            obj.Description = item.Description;
            obj.SanctionType = item.SanctionType;
            obj.SanctionTypeId = item.SanctionTypeId;
            await Commit();
            return Success();
        }

        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                Type = "Sanction",
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
