using System;

namespace Common.DisplayClasses
{
    public interface IMachineData
    {
        (string MachineName, DateTime DateTime, string Type, double Usage) GetData();
        string ToString();
    }
}