using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication3.Models.Category>? Category { get; set; }
        public DbSet<WebApplication3.Models.Products>? Products  { get; set; }
        public DbSet<restSakei.Models.Staf>? Staf { get; set; }
        public DbSet<WebApplication3.Models.ContactUsModel>? ContactUsModel { get; set; }
        public DbSet<WebApplication3.Models.ReservationModel>? ReservationModel { get; set; }
        public DbSet<WebApplication3.Models.ReviewModel>? ReviewModel { get; set; }

    }
}
