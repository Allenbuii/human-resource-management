
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class RecruitmentRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Recruitment>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var dep = await appDbContext.Recruitments.FindAsync(id);
            if (dep is null) return NotFound();

            appDbContext.Recruitments.Remove(dep);
            await Commit();
            return Success();
        }

        public async Task<List<Recruitment>> GetAll() => await appDbContext
            .Recruitments
            .AsNoTracking()
            .Include(b => b.Branch)
            .ThenInclude(d => d.Department)
            .ThenInclude(gd => gd.GeneralDepartment)
            .ToListAsync();
        public async Task<Recruitment> GetById(int id) => await appDbContext.Recruitments.FindAsync(id);

        public async Task<GeneralResponse> Insert(Recruitment item)
        {
            if (!await CheckName(item.Name!)) return new GeneralResponse(false, $"{item.Name} already added");
            appDbContext.Recruitments.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Recruitment item)
        {
            var recruitment = await appDbContext.Recruitments.FindAsync(item.Id);
            if (recruitment is null) return NotFound();
            recruitment.Name = item.Name;
            recruitment.Description = item.Description;
            recruitment.PostingDate = item.PostingDate;
            recruitment.ClosingDate = item.ClosingDate;
            recruitment.TotalPositions = item.TotalPositions;
            recruitment.BranchId = item.BranchId;
            recruitment.Branch = item.Branch;
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry recruitment not found");
        private static GeneralResponse Success() => new(true, "Process completed");
        private async Task<bool> CheckName(string name)
        {
            var item = await appDbContext.Recruitments.FirstOrDefaultAsync(x => x.Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
