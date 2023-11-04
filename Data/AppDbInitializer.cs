using System;
using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (context != null)
                {
                    context.Database.Migrate();
                    if (!context.Categories.Any())
                    {
                        context.Categories.AddRange(
                            new List<Category>
                            {
                                new Category()
                                {
                                    Name = "Comic",
                                    DisplayOrder = 1
                                },
                                new Category()
                                {
                                    Name = "Fiction",
                                    DisplayOrder = 2
                                },
                                new Category()
                                {
                                    Name = "Romantic",
                                    DisplayOrder = 3
                                },
                                new Category()
                                {
                                    Name = "Programming",
                                    DisplayOrder = 4
                                }
                            }
                        );
                        context.SaveChanges();
                    }

                    if (!context.CoverTypes.Any())
                    {
                        context.CoverTypes.AddRange(new List<CoverType>
                        {
                            new CoverType() {Name = "Hard cover"},
                            new CoverType() {Name = "Soft cover"}
                        });
                        context.SaveChanges();
                    }

                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(new List<Product>
                        {
                            new Product()
                            {
                                Title = "Superman comics",
                                Description = "Description of Superman comics cewcw cwcw wcwcw cewcwcw fewfew",
                                ISBN = "ISBN",
                                Author = "author 1",
                                Price = 12.99,
                                Price50 = 12,
                                Price100 = 11,
                                ImageUrl = "",
                                Category = context.Categories.Where(c => c.Name == "Comic").FirstOrDefault(),
                                CoverType = context.CoverTypes.Where(c => c.Name == "Soft cover").FirstOrDefault(),
                                Vaccine = context.Vaccines.Where(c => c.Name == "COVID-19 Vaccine").FirstOrDefault(),
                            }
                        });
                        context.SaveChanges();
                    }
                   
            }
        }
    }
}
