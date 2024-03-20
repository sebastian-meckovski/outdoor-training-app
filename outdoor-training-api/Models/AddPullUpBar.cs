using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PullupBars.Models
{
    public class AddPullUpBar
    {
        public int PosX { get; set; }

        public int PosY { get; set; }
        
        [Required, MinLength(10)]
        public string? Description { get; set; }
        [ForeignKey("UserId")]
        public virtual required Guid UserId { get; set;}
    }
}
