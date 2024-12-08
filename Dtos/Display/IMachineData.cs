using System;

namespace Common.DisplayClasses
{
    public interface IMachineData : IComparable<IMachineData>
    {
        DateTime GetDateTime();
        string GetMachineName();
        string ToString();
    }
}