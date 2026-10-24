
namespace BaseLibrary.Entities
{
    public class Degree : BaseEntity
    {
        public DateTime DegreeDate { get; set; }
        public string? DegreeInstitution { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        // Many to one relationship with TrainingProgram
        public TrainingProgram? TrainingProgram { get; set; }
        public int? TrainingProgramId { get; set; }

        // Many to one relationship with Employee
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
