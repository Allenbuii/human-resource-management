
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }

        // Many to one relationship with General Department
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        // One to many relationship with Branch
        [JsonIgnore]
        public List<Branch>? Branches { get; set; }
    }
}
