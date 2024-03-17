using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PullupBars.Models;


namespace PullupBars.Data
{
    public class PullupBarsAPIDbContext : IdentityDbContext
    {

        public PullupBarsAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<PullUpBar> PullupBars { get; set; }
    }
}




// public class ApplicationDbContext : IdentityDbContext  
// {  
//     public DbSet<ApplicationUser> ApplicationUsers { get; set; }  
//     public DbSet<ApplicationRole> ApplicationRoles { get; set; }  
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  
//         : base(options)  
//     {  
//     }  
//     protected override void OnModelCreating(ModelBuilder builder)  
//     {  
//         base.OnModelCreating(builder);   
//     }  
// }  
