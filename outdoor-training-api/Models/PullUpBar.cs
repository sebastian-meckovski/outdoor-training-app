using Microsoft.EntityFrameworkCore;

namespace PullupBars.Models
{
    public class PullUpBar
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public string? Description { get; set; }
    }
}
