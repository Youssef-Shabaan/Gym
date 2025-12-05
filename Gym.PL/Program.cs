using Gym.BLL.EmailSetting;
using Gym.BLL.Mapper;
using Gym.BLL.Service.Abstraction;
using Gym.BLL.Service.Implementation;
using Gym.DAL.DataBase;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Abstraction;
using Gym.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym.PL
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // load config files and environment variables
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //connection string
            var connectionString = builder.Configuration.GetConnectionString("GymConnection");

            builder.Services.AddDbContext<GymDbContext>(options => options.UseSqlServer(connectionString));

            // Identity Services
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<GymDbContext>()
                .AddDefaultTokenProviders();

            // Repositories Registration
            builder.Services.AddScoped<IAttendanceRepo, AttendanceRepo>();
            builder.Services.AddScoped<IMemberRepo, MemberRepo>();
            builder.Services.AddScoped<IMemberSessionRepo, MemberSessionRepo>();
            builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<ITrainerRepo, TrainerRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<IPlanRepo, PlanRepo>();
            builder.Services.AddScoped<IMemberPlanRepo, MemberPlanRepo>();

            // Services Registration
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IMemberSessionService, MemberSessionService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IMemberPlanService, MemberPlanService>();
            builder.Services.AddScoped<IPayPalService, PayPalService>();
            builder.Services.AddScoped<IUserService, UserService>();


            // Email Settings
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();


            // Sign with google
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });



            // Mapper
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            async Task CreateRolesAndAdmin(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var dbContext = serviceProvider.GetRequiredService<GymDbContext>();

                // Define roles
                string[] roles = { "Admin", "Member", "Trainer" };

                // Create roles if they don't exist
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Create a default admin user
                string adminEmail = "admin@gym.com";
                string adminPassword = "Admin@123";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User
                    {
                        UserName = "Admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newAdminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, "Admin");

                        // Create related Admin entity
                        var adminEntity = new Admin("System Admin", null, DAL.Enums.Gender.Male, 20, "Qena", newAdminUser.Id);
                        await dbContext.admins.AddAsync(adminEntity);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Create roles when the app starts
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await CreateRolesAndAdmin(services);
            }

            app.Run();
        }
    }
}
