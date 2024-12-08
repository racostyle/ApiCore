﻿using System;
namespace Common.DisplayClasses
{
    public class CPUData : IMachineData
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

        public int CompareTo(IMachineData other)
        {
            return _dateTime.CompareTo(other.GetDateTime());
        }

        public DateTime GetDateTime()
        {
            return _dateTime;
        }

        public string GetMachineName()
        {
            return _machineName;
        }
    }

    public class MemoryData : IMachineData
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
                    _usage = (u / t * 100).ToString("F2");
            }
        }

        public override string ToString()
        {
            var baseline = $"Machine Name: {_machineName}, Date: {_dateTime}, Type: {_type}{Environment.NewLine}";
            if (string.IsNullOrEmpty(_usage))
                return $"* {baseline}Used: {_context}GB";
            return $"* {baseline}Used: {_context}GB, Used Percentage: {_usage}%";
        }

        public int CompareTo(IMachineData other)
        {
            return _dateTime.CompareTo(other.GetDateTime());
        }

        public DateTime GetDateTime()
        {
            return _dateTime;
        }

        public string GetMachineName()
        {
            return _machineName;
        }
    }

    public class DiskData : IMachineData
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

        public int CompareTo(IMachineData other)
        {
            return _dateTime.CompareTo(other.GetDateTime());
        }

        public DateTime GetDateTime()
        {
            return _dateTime;
        }

        public string GetMachineName()
        {
            return _machineName;
        }
    }
}