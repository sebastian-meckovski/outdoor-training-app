using Microsoft.EntityFrameworkCore;
using OutdoorTraining.Models;

namespace PullupBars.Data
{
    public class PullupBarsAPIDbContext : DbContext
    {
        public PullupBarsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add Identity related model configuration
            base.OnModelCreating(modelBuilder);

            // Your fluent modeling here
        }
        public DbSet<PullUpBar> PullupBars { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
