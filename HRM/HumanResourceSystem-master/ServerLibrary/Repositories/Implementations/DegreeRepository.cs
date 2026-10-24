
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class DegreeRepository(AppDbContext appDbContext) : IDegreeInterface
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Degrees
                .FirstOrDefaultAsync(p => p.Id == id);
            if (item is null) return NotFound();

            appDbContext.Degrees.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Degree>> GetAll()
        {
            var degrees = await appDbContext.Degrees
                .AsNoTracking()
                .Include(p => p.TrainingProgram)
                .ThenInclude(i => i.Instructor)
                .Include(p => p.Employee)
                .ToListAsync();

            return degrees;
        }

        public async Task<List<Degree>> GetAllByTrainingProgramId(int trainingProgramId)
        {
            var degrees = await appDbContext.Degrees
                .AsNoTracking()
                .Include(p => p.TrainingProgram)
                .ThenInclude(i => i.Instructor)
                .Include(p => p.Employee)
                .Where(p => p.TrainingProgramId == trainingProgramId)
                .ToListAsync();

            return degrees;
        }

        public async Task<List<Degree>> GetAllByEmployeeId(int employeeId)
        {
            var degrees = await appDbContext.Degrees
                .AsNoTracking()
                .Include(p => p.TrainingProgram)
                .ThenInclude(i => i.Instructor)
                .Include(p => p.Employee)
                .Where(p => p.EmployeeId == employeeId)
                .ToListAsync();

            return degrees;
        }

        public async Task<Degree> GetById(int trainingProgramId, int employeeId)
        {
            var degree = await appDbContext.Degrees
                .Include(p => p.TrainingProgram)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(p => p.TrainingProgramId == trainingProgramId && p.EmployeeId == employeeId);

            return degree!;
        }

        public async Task<GeneralResponse> Insert(Degree item)
        {
            appDbContext.Degrees.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Degree item)
        {
            var deg = await appDbContext.Degrees.FindAsync(item.Id);
            if (deg is null) return NotFound();
            deg.Name = item.Name;
            deg.DegreeDate = item.DegreeDate;
            deg.DegreeInstitution = item.DegreeInstitution;
            deg.Description = item.Description;
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry degree not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
