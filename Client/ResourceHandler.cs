using Client.DataFetch;
using Common;
using Common.Dto;
using System.Text;
using System.Text.Json;

namespace Client
{
    internal class ResourceHandler
    {
        private readonly SimpleHttpClient _httpClient;
        private readonly IFetchData[] _fetchData;
        private readonly int _fetchInterval;
        private readonly string MachineName = Environment.MachineName;

        public ResourceHandler(SimpleHttpClient httpClient, int fetchInterval, params IFetchData[] fetchData)
        {
            _httpClient = httpClient;
            _fetchInterval = fetchInterval;
            _fetchData = fetchData;
        }

        internal async Task Work()
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(_fetchInterval));


                List<StringContent> contents = new List<StringContent>();
                foreach (var data in _fetchData)
                {
                    var json = await FetchData_ThenBuildJson(data);
                    contents.Add(json);
                }

                var result = await _httpClient.ExecuteAsync(contents);
                await Console.Out.WriteLineAsync(result);
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