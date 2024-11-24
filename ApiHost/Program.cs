using ApiHost.Database;

namespace ApiHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var settings = new Settings();


            var sqlExecutor = new DatabaseQueryExecutor();
            var dbUtils = new DatabaseUtils();
            var queries = new Queries();

            var sqlSeverName = settings.GetSqlServerName();

            using (var safetychecks = new DatabaseSafetyChecks(sqlExecutor, dbUtils, queries, sqlSeverName))
            {
                var result = await safetychecks.DoesServerExist();
                if (!result)
                    throw new Exception($"Could not connect to sqlServer: {sqlSeverName}");
                await safetychecks.CreateLogsDatabaseIfItDoesNotExist();
                await safetychecks.CreateLogsTableIfItDoesNotExist();
            }







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

            //for adding custom objects
            //var myLogger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<MyService>();
            //var preConfiguredService = new MyService(myLogger);
            //builder.Services.AddSingleton(preConfiguredService);


            builder.Services.AddSingleton<Settings>(); //single instance injected each time and persist for aplication lifetime
            //builder.Services.AddScoped<Settings>(); //new isntance per http request
            //builder.Services.AddTransient<Settings>(); //new instance per each request

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
}