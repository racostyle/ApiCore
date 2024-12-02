namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string address = "http://localhost:5337/api/Log";

            var worker = new ResourceFetcher(address);

            while (true)
            {
                await worker.Work();
            }
        }
    }
}
