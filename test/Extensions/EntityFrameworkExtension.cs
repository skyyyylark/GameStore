using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace test.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
