using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common
{
    public class SimpleHttpClient
    {
        private readonly string _address;

        public SimpleHttpClient(string address)
        {
            _address = address;
        }

        public async Task<string> ExecuteAsync(List<StringContent> contents = null)
        {
            if (contents == null)
                return await ExecuteWithSafetyChecks(Fetch());

            return await ExecuteWithSafetyChecks(Post(contents));

        }

        private async Task<string> Post(List<StringContent> contents)
        {
            var result = string.Empty;
            using (var client = new HttpClient())
            {
                foreach (var content in contents)
                {
                    var response = await client.PostAsync(_address, content);

                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> Fetch()
        {
            var result = string.Empty;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_address);
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        private async Task<string> ExecuteWithSafetyChecks(Task<string> task)
        {
            try
            {
                var result = await task.ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                return $"FATAL ERROR: {ex.Message}";
            }
        }
    }
}
