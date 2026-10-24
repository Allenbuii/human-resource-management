
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities
{
    public class Bonus : BaseEntity
    {
        [Required]
        public int BonusAmount { get; set; }
        [Required]
        public DateTime BonusDate { get; set; }

        [Required]
        public string? Reason { get; set; }

        // Many to one relationship with Employee
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}
