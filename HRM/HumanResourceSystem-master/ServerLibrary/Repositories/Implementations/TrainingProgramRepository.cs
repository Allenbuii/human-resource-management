
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class TrainingProgramRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<TrainingProgram>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.TrainingPrograms.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.TrainingPrograms.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<TrainingProgram>> GetAll() => await appDbContext
            .TrainingPrograms
            .AsNoTracking()
            .Include(i => i.Instructor)
            .ToListAsync();

        public async Task<TrainingProgram> GetById(int id) => await appDbContext.TrainingPrograms.FindAsync(id);

        public async Task<GeneralResponse> Insert(TrainingProgram item)
        {
            appDbContext.TrainingPrograms.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(TrainingProgram item)
        {
            var trainingProgram = await appDbContext.TrainingPrograms.FirstOrDefaultAsync(tp => tp.Id == item.Id);
            if (trainingProgram is null) return new GeneralResponse(false, "Training Program does not exist");

            trainingProgram.Name = item.Name;
            trainingProgram.Description = item.Description;
            trainingProgram.StartDate = item.StartDate;
            trainingProgram.EndDate = item.EndDate;
            trainingProgram.TrainingDay = item.TrainingDay;
            trainingProgram.Certificate = item.Certificate;

            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry training program not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
