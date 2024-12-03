

namespace Client.DataFetch
{
    internal class CpuUsage : IFetchData
    {
        private readonly string TYPE = "CPU";
        private ShellExecutor _shellExecutor;

        public CpuUsage(ShellExecutor shellExecutor)
        {
            _shellExecutor = shellExecutor;
        }

        public async Task<(string Type, string Data)> Fetch()
        {
            string arguments = @"
                for ($i = 0; $i -lt 3; $i++)
                {
                    $cpuLoad = Get-WmiObject -Class Win32_Processor | Measure-Object -Property LoadPercentage -Average | Select-Object -ExpandProperty Average
                    Write-Host ""$cpuLoad""
                    Start-Sleep -Seconds 1
                }";

            string result = await _shellExecutor.RunShellCommand(arguments);

            return (TYPE, BuildData(result));
        }

        private string BuildData(string result)
        {
            if (string.IsNullOrEmpty(result))
                return "ERROR";

            string[] results = BuildArray(result);

            if (results.Length == 0)
                return "ERROR";

            float average = 0;
            foreach (string line in results)
            {
                if (float.TryParse(line, out var usage))
                    average += usage;
            }

            return $"{average / results.Length}";
        }

        private string[] BuildArray(string result)
        {
            if (result.Contains(Environment.NewLine))
                return result.Split(Environment.NewLine);
            if (result.Contains("\n"))
                return result.Split("\n");

            return Array.Empty<string>();
        }
    }
}
