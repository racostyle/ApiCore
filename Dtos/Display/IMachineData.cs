using System;

namespace Common.DisplayClasses
{
    public interface IMachineData
    {
        (string MachineName, DateTime DateTime, string Type) GetData();
        string ToString();
    }
}