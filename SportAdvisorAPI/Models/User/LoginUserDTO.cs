using System.ComponentModel.DataAnnotations;

namespace SportAdvisorAPI.Models
{
    public class LoginUserDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}