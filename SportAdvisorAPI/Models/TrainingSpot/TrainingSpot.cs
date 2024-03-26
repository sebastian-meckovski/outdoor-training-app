using System.ComponentModel.DataAnnotations.Schema;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Models
{
    public class TrainingSpot
    {
        public required Guid Id { get; set; }
        public required DateTime DateAdded { get; set; }
        public required int PosX { get; set; }
        public required int PosY { get; set; }
        public string? Description { get; set; }

        public User? User { get; set; }
    }
}