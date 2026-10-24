
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Recruitment : BaseEntity
    {
        [Required]
        public string? Description { get; set; }
        [Required]
        public string JobName { get; set; } = string.Empty;
        [Required]
        public int TotalPositions { get; set; }
        [Required]
        public DateTime PostingDate { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }
        public int NumberOfDays => (ClosingDate - PostingDate).Days;

        //Many to one relationship with Branch
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }

        // Relationship: One to many with Candidate
        [JsonIgnore]
        public List<Candidate>? Candidates { get; set; }
    }
}
