using Gym.DAL.DataBase;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Gym.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //connection string
            var connectionString = builder.Configuration.GetConnectionString("GymConnection");

            builder.Services.AddDbContext<GymDbContext>(options =>options.UseSqlServer(connectionString));

            
            builder.Services.AddScoped<ITrainerRepo, TrainerRepo>();




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

            app.Run();
        }
    }
}
