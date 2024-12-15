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
            var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));

            if (settings == null)
                throw new Exception($"Could not read configuration");

            if (!settings.TryGetValue("Address", out string address))
                throw new Exception($"Invalid configuration");

            if (!int.TryParse(settings["FetchInterval"], out var fetchInterval))
            {
                fetchInterval = 10;
            }

            var concreteFetchers = FetchBuilder(settings);

            var worker = new ResourceHandler(
                new SimpleHttpClient(address),
                fetchInterval,
                concreteFetchers);

            Console.WriteLine($"Using address: {address}");
            Console.WriteLine($"Working...");

            while (true)
            {
                await worker.Work();
            }
        }

        private static IFetchData[] FetchBuilder(Dictionary<string, string> settings)
        {
            var list = new List<IFetchData>();

            if (IsKeyOptionTrue(settings, "FetchCPU"))
                list.Add(new CpuUsage(new ShellExecutor()));
            if (IsKeyOptionTrue(settings, "FetchRAM"))
                list.Add(new MemoryUsage(new ShellExecutor()));
            if (IsKeyOptionTrue(settings, "FetchDISK"))
                list.Add(new DiskUsage());

            return list.ToArray();
        }

        private static bool IsKeyOptionTrue(Dictionary<string, string> settings, string key)
        {
            if (settings.ContainsKey(key))
            {
                if (settings[key].Trim().Equals("1"))
                    return true;
            }
            return false;
        }
    }
}
