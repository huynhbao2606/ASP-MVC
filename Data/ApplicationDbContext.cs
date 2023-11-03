using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASP_MVC.Models;

namespace ASP_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Type>().HasData(
                new Models.Type
                { Id = 1, Name = "Inactivated", CreatedAt = new DateTime(2023, 10, 31, 2, 40, 50) },
                new Models.Type
                { Id = 2, Name = "Live-attenuated", CreatedAt = new DateTime(2023, 10, 31, 3, 44, 12) },
                new Models.Type
                { Id = 3, Name = "Messenger RNA (mRNA)", CreatedAt = new DateTime(2023, 10, 31, 4, 55, 23) },
                new Models.Type
                { Id = 4, Name = "Subunit, recombinant, polysaccharide, and conjugate", CreatedAt = new DateTime(2023, 10, 31, 5, 22, 34) });
        }
    }
}
