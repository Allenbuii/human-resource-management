
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Sanction : OtherBaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Punishment { get; set; } = string.Empty;
        [Required]
        public DateTime PunishmentDate { get; set; }
        public string? Description { get; set; } = string.Empty;

        // Many to one relationship with Sanction Type
        public SanctionType? SanctionType { get; set; }
        public int SanctionTypeId { get; set; }
    }
}
