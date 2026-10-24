using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class EmployeeTransfer : BaseEntity
    {
        [Required]
        public string? NewPosition { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public string? Reason { get; set; }
        public string? EmployeeRequestName { get; set; }
        public int? EmployeeRequest { get; set; }

        // Many to one relationship with Employee
        public Employee? Employee { get; set; }
        public int? EmployeeId { get; set; }

        // Many to one relationship with Branch
        public Branch? Branch { get; set; }
        public int? BranchId { get; set; }
    }
}
