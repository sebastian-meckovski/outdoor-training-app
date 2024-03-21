using Microsoft.AspNetCore.Identity;

namespace OutdoorTraining.Models
{
    public class AppUser : IdentityUser
    {
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
