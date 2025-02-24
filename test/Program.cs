using Abstractions.Interfaces.Services;
using BLL.Services;
using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Serilog;
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

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
