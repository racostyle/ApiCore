using System;
using System.Globalization;
namespace Common.DisplayClasses
{
    public class MemoryData : IMachineData, IComparable<string>
    {
        public readonly string _machineName;
        public readonly DateTime _dateTime;
        private readonly string _type;
        public readonly string _context;
        public readonly string _usage;

        public MemoryData(string machineName, string dateTime, string type, string used, string total)
        {
            _machineName = machineName;
            _dateTime = DateTime.Parse(dateTime);
            _type = type;
            _context = used;
            _usage = string.Empty;

            if (string.IsNullOrEmpty(total))
                return;

            if (double.TryParse(used, out double u))
            {
                if (double.TryParse(total, out double t))
                    _usage = (u / t * 100).ToString("F2").Replace(",", ".");
            }
        }

        public override string ToString()
        {
            var baseline = $"Machine Name: {_machineName}, Date: {_dateTime}, Type: {_type}{Environment.NewLine}";
            if (string.IsNullOrEmpty(_usage))
                return $"* {baseline}Used: {_context}GB";
            return $"* {baseline}Used: {_context}GB, Used Percentage: {_usage}%";
        }

        public int CompareTo(string other)
        {
            return _machineName.CompareTo(other);
        }

        public (string MachineName, DateTime DateTime, string Type, double Usage) GetData()
        {
            return (_machineName, _dateTime, _type, double.Parse(_usage, CultureInfo.InvariantCulture));
        }
    }
}
