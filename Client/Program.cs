using Client.DataFetch;
using Common;
using System.Net;
using System.Text.Json;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));

            if (json == null)
                throw new Exception($"Could not read configuration");

            if (!json.TryGetValue("Address", out string address))
                throw new Exception($"Invalid configuration");

            if (!int.TryParse(json["FetchInterval"], out var fetchInterval))
            {
                fetchInterval = 10;
            }

            var worker = new ResourceHandler(
                new SimpleHttpClient(address),
                fetchInterval,
                new CpuUsage(new ShellExecutor()),
                new DiskUsage(),
                new MemoryUsage(new ShellExecutor()));

            Console.WriteLine($"Using address: {address}");
            Console.WriteLine($"Working...");

            while (true)
            {
                await worker.Work();
            }
        }
    }
}
