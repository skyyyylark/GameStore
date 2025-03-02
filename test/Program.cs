using Abstractions.Interfaces.Services;
using BLL.Services;
using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Entities;
using Serilog;
using System.Globalization;
using test.Extensions;

namespace test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.AddSerilogLogging();

            builder.Services.AddEntityFramework(builder.Configuration);

            builder.Services.AddDataSourceServices();

            builder.Services.AddBllServices();

            builder.Services.AddIdentityServices();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerConfiguration();

            builder.Services.AddLocalization(options => options.ResourcesPath = "..\\Common\\Resources");

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            var app = builder.Build();

            var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            app.Services.SeedIdentityData();

            app.AddExceptionHandler();
            app.InitMapping();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
