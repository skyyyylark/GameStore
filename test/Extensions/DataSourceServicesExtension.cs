using Abstractions.Interfaces.DataSources;
using BLL.Services;
using DAL.DataSource;

namespace test.Extensions
{
    public static class DataSourceServicesExtension
    {
        public static void AddDataSourceServices(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IGenericDataSource<>), typeof(GenericDataSource<>))
                .AddScoped<ICategoryDataSource, CategoryDataSource>()
                .AddScoped<IGameDataSource, GameDataSource>();
        }
    }
}
