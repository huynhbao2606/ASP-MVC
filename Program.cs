using ASP_MVC.Dao;
using ASP_MVC.Dao.IRepository;
using ASP_MVC.Data;
using ASP_MVC.Models;
using ASP_MVC.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

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

            builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

           

            builder.Services.AddRazorPages();
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         

            //Config session 

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.IsEssential = true;
            });



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

            app.UseAuthentication();;

            app.MapRazorPages();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}