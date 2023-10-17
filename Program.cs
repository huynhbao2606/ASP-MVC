using ASP_MVC.Dao;
using ASP_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DBContext
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                 builder.Configuration.GetConnectionString("Entity")
            ));


           builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
           builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            //AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}