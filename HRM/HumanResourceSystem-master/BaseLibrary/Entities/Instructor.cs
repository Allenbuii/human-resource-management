
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Instructor : BaseEntity
    {
        [Required]
        public string? Email { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string? TelephoneNumber { get; set; } = string.Empty;
        [Required]
        public string? Address { get; set; } = string.Empty;
        [Required]
        public string? Specialization { get; set; } = string.Empty;

        // one to many relationship with TrainingProgram
        [JsonIgnore]
        public List<TrainingProgram>? TrainingPrograms { get; set; }
    }
}
