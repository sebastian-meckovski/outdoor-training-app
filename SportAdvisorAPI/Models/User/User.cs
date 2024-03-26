using Microsoft.AspNetCore.Identity;

namespace SportAdvisorAPI.Models
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }

    }
}