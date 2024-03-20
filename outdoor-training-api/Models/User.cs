﻿namespace OutdoorTraining.Models
{
    public class User
    {
        public Guid Id { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
