using Microsoft.EntityFrameworkCore;
using ASP_MVC.Models;


namespace ASP_MVC.Data
{
    public class ApplicationDbContext : DbContext
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
