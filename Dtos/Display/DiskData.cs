using System;
namespace Common.DisplayClasses
{
    public class DiskData : IMachineData, IComparable<string>
    {
        public readonly string _machineName;
        public readonly DateTime _dateTime;
        private readonly string _type;
        private readonly string _diskName;
        public readonly string _context;
        public readonly string _usage;

        public DiskData(string machineName, string dateTime, string type, string diskName, string used, string total)
        {
            _machineName = machineName;
            _dateTime = DateTime.Parse(dateTime);
            _type = type;
            _diskName = diskName;
            _context = used;
            _usage = string.Empty;

            if (double.TryParse(used, out double u))
            {
                if (double.TryParse(total, out double t))
                    _usage = (u / t * 100).ToString("F2");
            }
        }

        public override string ToString()
        {
            var baseline = $"Machine Name: {_machineName}, Date: {_dateTime}, Type: {_type}{Environment.NewLine}";
            return $"* {baseline}Disk: {_diskName}, Used: {_context}GB, Used Percentage: {_usage}%";
        }

        public int CompareTo(string other)
        {
            return _machineName.CompareTo(other);
        }

        public (string MachineName, DateTime DateTime, string Type) GetData()
        {
            return (_machineName, _dateTime, _type);
        }
    }
}
