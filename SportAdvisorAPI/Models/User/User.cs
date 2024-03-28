using Microsoft.AspNetCore.Identity;

namespace SportAdvisorAPI.Models
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public Guid? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
        public Guid? VerifyToken { get; set; }
    }
}