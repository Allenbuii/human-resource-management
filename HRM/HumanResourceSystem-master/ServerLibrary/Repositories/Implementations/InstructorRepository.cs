
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class InstructorRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Instructor>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var item = await appDbContext.Instructors.FindAsync(id);
            if (item is null) return NotFound();

            appDbContext.Instructors.Remove(item);
            await Commit();
            return Success();
        }

        public async Task<List<Instructor>> GetAll()
        {
            var instructors = await appDbContext.Instructors
                .AsNoTracking()
                .ToListAsync();

            return instructors;
        }

        public async Task<Instructor> GetById(int id) => await appDbContext.Instructors.FindAsync(id);

        public async Task<GeneralResponse> Insert(Instructor item)
        {
            appDbContext.Instructors.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Instructor item)
        {
            var instructor = await appDbContext.Instructors.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (instructor is null) return new GeneralResponse(false, "Instructor does not exist");

            instructor.Name = item.Name;
            instructor.Email = item.Email;
            instructor.TrainingPrograms = item.TrainingPrograms;

            await appDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private static GeneralResponse NotFound() => new(false, "Sorry instructor not found");
        private static GeneralResponse Success() => new(true, "Process completed");
    }
}
