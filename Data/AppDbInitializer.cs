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
                    if (!context.Vaccines.Any())
                    {
                        context.Vaccines.AddRange(new List<Vaccine>
    {
        new Vaccine
        {
            Name = "COVID-19 Vaccine",
            CountryOfManufacture = "USA",
            ExpirationDate = DateTime.Now.AddMonths(12),
            Price = 29.99,
            Type = new @Models.Type { Name = "Viral" }, 
        }
        // Add more vaccines as needed
    });
                        context.SaveChanges();
                    }

                    if (!context.Types.Any())
                    {
                        context.Types.AddRange(new List<@Models.Type>
    {
        new @Models.Type
        {
            Name = "Bacterial",
        },
        new @Models.Type
        {
            Name = "Viral",
        }
        // Add more types as needed
    });
                        context.SaveChanges();
                    }

                    if (!context.VaccinationSchedules.Any())
                    {
                        context.VaccinationSchedules.AddRange(new List<VaccinationSchedule>
    {
        new VaccinationSchedule
        {
            Name = "COVID-19 Vaccination Schedule",
            VaccinationDates = "Monthly",
            Vaccine = context.Vaccines.First(v => v.Name == "COVID-19 Vaccine"),
        }
        // Add more vaccination schedules as needed
    });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
