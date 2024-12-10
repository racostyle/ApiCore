

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
                $total = 0
                for ($i = 0; $i -lt 5; $i++)
                {
                    $cpuUsage = (Get-Counter '\Processor(_Total)\% Processor Time').CounterSamples.CookedValue
                    $roundedCpuUsage = $cpuUsage
                    $total = $total + $roundedCpuUsage
                    Start-Sleep -Seconds 1
                }
                $total = $total/5
                $totalFormatted = $total.ToString('F2', [System.Globalization.CultureInfo]::InvariantCulture)
                Write-Host $totalFormatted";


            string result = await _shellExecutor.RunShellCommand(arguments);

            return (TYPE, BuildData(result));
        }

        private string BuildData(string result)
        {
            if (string.IsNullOrEmpty(result))
                return "ERROR";

            return $"{result}";
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
