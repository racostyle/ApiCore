namespace Client.DataFetch
{
    internal class DiskUsage : IFetchData
    {
        private readonly long TO_GB = 1024 * 1024 * 1024;
        private readonly string TYPE = "DISK";

        public DiskUsage()
        {
            Console.WriteLine($"Fetch '{TYPE}' Enabled.");
        }

        public Task<(string Type, string Data)> Fetch()
        {
            var data = new List<string>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    var diskName = $"{drive.Name}";
                    var totalSize = drive.TotalSize;
                    var used = drive.TotalSize - drive.TotalFreeSpace;
                    data.Add($"{diskName}||{used / TO_GB}||{totalSize / TO_GB}");
                }
            }
            return Task.FromResult((TYPE, data.Aggregate((x, next) => $"{x}||||{next}")));
        }
    }
}
