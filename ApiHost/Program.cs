
using System.Text.Json;

namespace ApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            //builder.WebHost.ConfigureKestrel(serverOptions =>
            //{
            //    serverOptions.ListenAnyIP(5337); // HTTP on port 5337
            //    serverOptions.ListenAnyIP(8337, listenOptions =>
            //    {
            //        listenOptions.UseHttps(); // HTTPS on port 8337
            //    });
            //});

            builder.Services.AddSingleton<Settings>(); //single instance injected each time and persist for aplication lifetime
            //builder.Services.AddScoped<Settings>(); //single  
            //builder.Services.AddTransient<Settings>();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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

    public class Settings
    {
        private const string SQL_SERVER_NAME = "SqlServer";
        private const string CONFIG_NAME = "appsettings.Secrets.json";

        private Dictionary<string, string> _config;

        public Settings()
        {
            LoadSettings();
        }

        internal void LoadSettings()
        {
            var json = File.ReadAllText(CONFIG_NAME);
            if (!string.IsNullOrEmpty(json))
            {
                _config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
        }
    }
}