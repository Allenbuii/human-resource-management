
namespace BaseLibrary.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
