
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class TrainingProgram : BaseEntity
    {
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DayOfWeek TrainingDay { get; set; }

        // Certificate information
        public string? Certificate { get; set; } = string.Empty;

        // Relationship: Many to many with Employee through Degree
        [JsonIgnore]
        public List<Degree>? Degrees { get; set; }

        // Relationship: Many to one with Instructor
        public Instructor? Instructor { get; set; }
        public int InstructorId { get; set; }
    }
}
