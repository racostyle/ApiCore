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
                Write-Host ""$([math]::Round($usedRAM / 1MB, 2))""
                Write-Host ""$([math]::Round($totalRAM / 1MB, 2))""";

            string result = await _shellExecutor.RunShellCommand(arguments);

            result = result.Replace("\n", "||").Replace("\r", string.Empty).Trim();

            return (TYPE, result);
        }
    }
}