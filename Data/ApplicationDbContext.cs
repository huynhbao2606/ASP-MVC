using Microsoft.EntityFrameworkCore;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ASP_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products
        {
            get; set;
        }
    }
}
