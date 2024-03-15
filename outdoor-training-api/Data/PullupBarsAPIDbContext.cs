//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PullupBars.Models;

namespace PullupBars.Data
{
    public class PullupBarsAPIDbContext : DbContext
    {
        public PullupBarsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PullUpBar> PullupBars { get; set; }
    }
}
