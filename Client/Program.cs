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

            string address = json["Address"];

            var worker = new ResourceFetcher(address);

            Console.WriteLine($"Using address: {address}");
            Console.WriteLine($"Working...");
            while (true)
            {
                await worker.Work();
            }
        }
    }
}
