
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }
        public string? Location { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }


        // One to many relationship with Department
        [JsonIgnore]
        public List<Department>? Departments { get; set; }
    }
}
