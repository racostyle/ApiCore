using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.DataFetch
{
    internal class MemoryUsage : IFetchData
    {
        private ShellExecutor _shellExecutor;
        private readonly string TYPE = "RAM";

        public MemoryUsage(ShellExecutor shellExecutor)
        {
            _shellExecutor = shellExecutor;
        }

        public async Task<(string Type, string Data)> Fetch()
        {
            string arguments = @"
                $totalRAM = (Get-CimInstance -ClassName Win32_OperatingSystem).TotalVisibleMemorySize
                $freeRAM = (Get-CimInstance -ClassName Win32_OperatingSystem).FreePhysicalMemory
                $usedRAM = $totalRAM - $freeRAM
                $usedRAMFormatted = [math]::Round($usedRAM / 1MB, 2).ToString('F2', [System.Globalization.CultureInfo]::InvariantCulture)
                $totalRAMFormatted = [math]::Round($totalRAM / 1MB, 2).ToString('F2', [System.Globalization.CultureInfo]::InvariantCulture)
                Write-Host $usedRAMFormatted
                Write-Host $totalRAMFormatted
            ";


            string result = await _shellExecutor.RunShellCommand(arguments);

            result = result.Replace("\n", "||").Replace("\r", string.Empty).Trim();

            return (TYPE, result);
        }
    }
}