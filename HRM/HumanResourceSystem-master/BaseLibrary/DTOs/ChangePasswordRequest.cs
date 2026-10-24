
namespace BaseLibrary.DTOs
{
    public class ChangePasswordRequest
    {
        public int? Id { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
