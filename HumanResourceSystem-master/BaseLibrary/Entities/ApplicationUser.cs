
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Account security
        public DateTime? LockoutEndTime { get; set; }
        public DateTime? FailedLoginStart { get; set; }
        public int FailedLoginAttempts { get; set; }

        // One to one relationship with Employee
        [JsonIgnore]
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
