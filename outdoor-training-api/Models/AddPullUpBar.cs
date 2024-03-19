using System.ComponentModel.DataAnnotations;

namespace PullupBars.Models
{
    public class AddPullUpBar
    {
        public int PosX { get; set; }

        public int PosY { get; set; }
        
        [Required, MinLength(10)]
        public string? Description { get; set; }
    }
}
