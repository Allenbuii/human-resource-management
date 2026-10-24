
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class VacationType : BaseEntity
    {
        public string? Description { get; set; } = string.Empty;
        // Many to one relationship with Vacation
        [JsonIgnore]
        public List<Vacation>? Vacations { get; set; }
    }
}
