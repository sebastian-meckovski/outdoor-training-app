using System.ComponentModel.DataAnnotations;

namespace OutdoorTraining.Models
{
    public class ResetPasswordRequest
    {
        public required string Token { get; set; }
        public required string UserId { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
