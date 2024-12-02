
using System.Text.Json;
using System.Text;
using ApiHost.DTO;

namespace Client
{
    internal class ResourceFetcher
    {
        private readonly string _address;

        public ResourceFetcher(string address)
        {
            _address = address;
        }

        internal async Task Work()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(60));

                var dto = new LogDTO(Environment.MachineName, "content");

                var json = JsonSerializer.Serialize(dto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var response = await client.PostAsync(_address, data);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error while sending request: {ex}");
            }
        }
    }
}