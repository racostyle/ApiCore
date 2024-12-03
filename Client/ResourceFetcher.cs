using Client.DataFetch;
using Dtos;
using System.Text;
using System.Text.Json;

namespace Client
{
    internal class ResourceFetcher
    {
        private readonly string _address;
        private readonly IFetchData[] _fetchData;
        private readonly string MachineName = Environment.MachineName;

        public ResourceFetcher(string address, params IFetchData[] fetchData)
        {
            _address = address;
            _fetchData = fetchData;
        }

        internal async Task Work()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(60));


                List<StringContent> contents = new List<StringContent>();
                foreach (var data in _fetchData)
                {
                    var json = await FetchData_ThenBuildJson(data);
                    contents.Add(json);
                }

                using (var client = new HttpClient())
                {
                    foreach (var content in contents)
                    {
                        var response = await client.PostAsync(_address, content);

                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending request: {ex}");
            }
        }

        private async Task<StringContent> FetchData_ThenBuildJson(IFetchData data)
        {
            var result = await data.Fetch();
            var dto = new LogDTO(MachineName, "INFO", result.Type, result.Data);

            var json = JsonSerializer.Serialize(dto);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}