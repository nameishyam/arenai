namespace Server.Domain.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? Otp { get; set; }
        public DateTime? OtpExpiry { get; set; }
    }
}