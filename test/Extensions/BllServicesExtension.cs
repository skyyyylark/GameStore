using Abstractions.Interfaces.DataSources;
using Abstractions.Interfaces.Services;
using BLL.Services;
using DAL.DataSource;

namespace test.Extensions
{
    public static class BllServicesExtension
    {
        public static void AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
