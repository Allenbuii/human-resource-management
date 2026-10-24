
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType : BaseEntity
    {
        public string? Description { get; set; } = string.Empty;
        // Many to one relationship with Vacation
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}
