using Bookify.BookingRepositary;
using Bookify.GuestRepositary;
using Bookify.Models;
using Bookify.RoomRepositary;
using Bookify.UserRepositary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Bookify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // identity
            builder.Services.AddDbContextPool<HotelDbContext>(options =>
                                                              options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ThabitConnection")));
            builder.Services.AddScoped<IRoomRepo, RoomRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IGuestRepo, GuestRepo>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
              options =>
              {
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireDigit = false;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequiredLength = 4;
              }
              ).AddEntityFrameworkStores<HotelDbContext>();


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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Custmer}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
