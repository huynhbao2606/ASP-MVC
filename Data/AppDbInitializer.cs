using ASP_MVC.Models;

namespace ASP_MVC.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService <ApplicationDbContext>();

                if (context != null)
                {
                    context.Database.EnsureCreated();
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
                }
            }
        }
    }
}
