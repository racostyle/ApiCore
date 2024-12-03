using System.Diagnostics;

namespace Client.DataFetch
{
    internal class ShellExecutor
    {
        internal async Task<string> RunShellCommand(string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            await process.WaitForExitAsync();

            if (!string.IsNullOrWhiteSpace(error))
            {
                throw new Exception($"Error: {error}");
            }

            return output.Trim();
        }
    }
}
