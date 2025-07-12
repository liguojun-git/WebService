using MCCMWebServiceApp.Common.Utilities;
using MCCMWebServiceApp.Infrastructure.Interfaces;
using MCCMWebServiceApp.Infrastructure.Repositories;
using MCCMWebServiceApp.Infrastructure.SqlContext;
using MCCMWebServiceApp.Web.Filter;
using Web.Filiter;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<AppParameter>(builder.Configuration.GetSection("AppParameter"));

            builder.Logging.ClearProviders();
            builder.Logging.AddLog4Net("Config/log4net.config");

            builder.Services.AddControllers(options =>
            {
                // LogFilter
                options.Filters.Add<LogFilter>();
                // ExceptionFilter
                options.Filters.Add<ExceptionFilter>();
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<LogFilter>();

            builder.Services.AddScoped<MCCMWebServiceApp.Infrastructure.SqlContext.DapperHelper>();

            builder.Services.AddScoped<IUserTestRepositories, UserTestRepositories>();
            builder.Services.AddScoped<MCCMWebServiceApp.Services.Interfaces.IUserTestService, MCCMWebServiceApp.Services.Services.UserTestService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var log4netLogger = app.Services.GetRequiredService<ILogger<Program>>();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                log4netLogger.LogError("データベース接続文字列が空白です。");
            }
            else
            {
                Dbcontext.Initialize(connectionString);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            log4netLogger.LogInformation("MCCMWebServiceApp is runing");

            app.Run();
        }
    }
}
