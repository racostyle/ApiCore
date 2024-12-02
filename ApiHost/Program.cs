using ApiHost.Database;

namespace ApiHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var settings = new Settings();

            var sqlHandler = new SqlHandler(
                new DatabaseQueryExecutor(),
                new DatabaseUtils(),
                new Queries(),
                settings.GetSqlServerName());

            using (var safetychecks = new DatabaseSafetyChecks(sqlHandler))
            {
                var result = await safetychecks.DoesServerExist();
                if (!result)
                    throw new Exception($"Could not connect to sqlServer: {settings.GetSqlServerName()}");
                await safetychecks.CreateLogsDatabaseIfItDoesNotExist();
                await safetychecks.CreateLogsTableIfItDoesNotExist();
            }

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5337); 
            });

            builder.Services.AddSingleton<Settings>(); 
            builder.Services.AddSingleton(sqlHandler); 

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

            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}