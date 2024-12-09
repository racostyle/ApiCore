using System;
namespace Common.DisplayClasses
{
    public class CPUData : IMachineData, IComparable<string>
    {
        private readonly DateTime _dateTime;
        public readonly string _machineName;
        private readonly string _type;
        public readonly string _context;

        public CPUData(string machineName, string dateTime, string type, string context)
        {
            _machineName = machineName;
            _dateTime = DateTime.Parse(dateTime);
            _type = type;
            _context = context;
        }

        public override string ToString()
        {
            var baseline = $"Machine Name: {_machineName}, Date: {_dateTime}, Type: {_type}{Environment.NewLine}";
            return $"* {baseline}Used Percentage: {_context}%";
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
