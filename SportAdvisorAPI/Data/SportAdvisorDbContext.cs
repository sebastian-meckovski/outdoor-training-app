using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportAdvisorAPI.Data.Configurations;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Data
{
    public class SportAdvisorDbContext : IdentityDbContext<User>
    {
        public SportAdvisorDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TrainingSpot> TrainingSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}