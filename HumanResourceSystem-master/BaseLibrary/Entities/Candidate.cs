
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Candidate : BaseEntity
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; } = string.Empty;
        [Required]
        public DateTime ApplicationDate { get; set; }
        public string? Status { get; set; }
        [Required]
        public string? CVLink { get; set; }

        //Many to one relationship with Recruitment
        public Recruitment? Recruitment { get; set; }
        public int RecruitmentId { get; set; }
    }
}
