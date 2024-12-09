using Common.DisplayClasses;
using System;

namespace Common.Display
{
    public static class MachineDataExtensions
    {
        public static string GetMachineName(this IMachineData input)
        {
            return input.GetData().MachineName;
        }

        public static DateTime GetDateTime(this IMachineData input)
        {
            return input.GetData().DateTime;
        }

        public static string GetLogType(this IMachineData input)
        {
            return input.GetData().Type;
        }

        public static double GetUsagePercent(this IMachineData input)
        {
            return input.GetData().Usage;
        }
    }
}
