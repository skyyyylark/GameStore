using BLL.Services;
using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;

namespace test.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                });

            services.AddAuthorization();
        }

        public static void SeedIdentityData(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            IdentitySeeder.SeedSuperAdmin(userManager, roleManager);
        }
    }
}
