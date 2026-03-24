
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using User_Registration_System.Data.DBContexts;
using User_Registration_System.Shared;
using User_Registration_System.Shared.Middlewares;

namespace User_Registration_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddTransient<GlobalExceptionHandler>();



            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseDefaultFiles(); 
            app.UseStaticFiles();

            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<ApplicationDbContext>();

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = service.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "Database Migration Failed: {Message}", ex.Message);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
