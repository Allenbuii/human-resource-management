
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string CivilId { get; set; } = string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;
        [Required]
        public string JobName { get; set; } = string.Empty;
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; } = string.Empty;
        [Required]
        public string Photo { get; set; } = string.Empty;
        public string? Other { get; set; }

        // One-to-one relationship with ApplicationUser
        public ApplicationUser? ApplicationUser { get; set; }

        // Many to one relationship with Branch
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }
        // Many to one relationship with Town
        public Town? Town { get; set; }
        public int TownId { get; set;}

        // Many to many relationship with TrainingProgram through Degree
        [JsonIgnore]
        public List<Degree>? Degrees { get; set; }

        // One-to-many relationship with PeriodicEvaluation
        [JsonIgnore]
        public List<PeriodicEvaluation>? PeriodicEvaluations { get; set; }

        // One-to-many relationship with Bonus
        [JsonIgnore]
        public List<Bonus>? Bonuses { get; set; }

        // One-to-many relationship with EmployeeTransfer
        [JsonIgnore]
        public List<EmployeeTransfer>? EmployeeTransfers { get; set; }
    }
}
