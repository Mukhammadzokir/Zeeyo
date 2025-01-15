using Serilog;
using Zeeyo.Api.Extensions;
using Zeeyo.Data.DbContexts;
using Zeeyo.Api.MiddleWares;
using Zeeyo.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Zeeyo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Logger 
        var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        // swagger set up
        builder.Services.AddSwaggerService();

        // JWT service
        builder.Services.AddJwtService(builder.Configuration);

        //// ServiceExtension
        builder.Services.AddCustomService();

        //// Db Connection
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


        builder.Services.AddMemoryCache();

        var app = builder.Build();

        // Getting wwwroot path
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        // Init accessor
        //app.InitAccessor();

        //// MiddleWare
        app.UseMiddleware<ExceptionHandlerMiddleWare>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
