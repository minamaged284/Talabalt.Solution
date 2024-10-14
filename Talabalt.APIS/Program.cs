

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabalt.APIS.Errors;
using Talabalt.APIS.Extensions;
using Talabalt.APIS.Helpers;
using Talabalt.APIS.Middlewares;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Repository.Data;

namespace Talabalt.APIS
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.applicationServices();


            var app = builder.Build();



            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreDbContextSeed.seedAsync(_dbcontext);
            }
            catch (Exception ex)
            {

              var logger =  loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error during migration");

            }

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.addSwaggerMiddleware();
            }
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();

            
        }
    }
}
