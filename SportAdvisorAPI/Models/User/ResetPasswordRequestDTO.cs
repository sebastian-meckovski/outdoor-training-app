namespace SportAdvisorAPI.Models
{
    public class ResetPasswordRequestDTO
    {
        public string? Email { get; set; }
        public string? ResetToken { get; set; }
        public string? NewPassword { get; set; }
    }
}