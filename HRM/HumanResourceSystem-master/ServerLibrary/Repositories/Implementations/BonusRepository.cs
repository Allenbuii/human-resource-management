
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class BonusRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Bonus>, IBonusInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Bonuses.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Bonuses.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Bonus>> GetAll()
        {
            var bonuses = await appDbContext.Bonuses
                .AsNoTracking()
                .Include(e => e.Employee)
                .ToListAsync();

            return bonuses;
        }

        public async Task<List<Bonus>> GetAllByEmployeeId(int employeeid)
        {
            var bonuses = await appDbContext.Bonuses
                .AsNoTracking()
                .Where(p => p.EmployeeId == employeeid)
                .ToListAsync();

            return bonuses;
        }

        public async Task<Bonus> GetById(int id) => await appDbContext.Bonuses.FindAsync(id);

        public async Task<GeneralResponse> Insert(Bonus item)
        {
            appDbContext.Bonuses.Add(item);

            string message = "You have a new bonus added by your supervisor.";
            await AddNotification(item.EmployeeId, message);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Bonus item)
        {
            var bonus = await appDbContext.Bonuses.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (bonus is null) return new GeneralResponse(false, "Bonus does not exist");

            bonus.Name = item.Name;
            bonus.BonusAmount = item.BonusAmount;
            bonus.BonusDate = item.BonusDate;
            bonus.Reason = item.Reason;

            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        public async Task AddNotification(int employeeId, string message)
        {
            var notification = new Notification
            {
                EmployeeId = employeeId,
                Message = message,
                Type = "Bonus",
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            appDbContext.Notifications.Add(notification);
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry instructor not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
