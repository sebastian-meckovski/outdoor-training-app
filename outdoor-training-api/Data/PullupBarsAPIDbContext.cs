using Microsoft.EntityFrameworkCore;
using PullupBars.Models;
using OutdoorTraining.Models;

namespace PullupBars.Data
{
    public class PullupBarsAPIDbContext : DbContext
    {
        public PullupBarsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PullUpBar> PullupBars { get; set; }
        public DbSet<User> Users => Set<User>();

    }
}
