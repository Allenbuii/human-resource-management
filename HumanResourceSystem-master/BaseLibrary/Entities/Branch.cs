
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {
        public int SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public int RequiredEmployees { get; set; }
        public int CurrentEmployees { get; set; }
        public int CurrentEmployeeTransfers { get; set; }

        //Many to one relationship with Department
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        // Relationship: One to many with Employee
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        // Relationship: One to many with Recruitment
        [JsonIgnore]
        public List<Recruitment>? Recruitments { get; set; }

        // Relationship: One to many with EmployeeTransfer
        [JsonIgnore]
        public List<EmployeeTransfer>? EmployeeTransfers { get; set; }
    }
}
