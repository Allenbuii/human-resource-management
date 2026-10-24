
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class OvertimeType : BaseEntity
    {
        public string? Description { get; set; } = string.Empty;
        // Many to one relationship with Overtime
        [JsonIgnore]
        public List<Overtime>? Overtimes { get; set; }
    }
}
