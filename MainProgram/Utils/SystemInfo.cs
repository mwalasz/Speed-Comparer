using System;
using System.Linq;
using System.Management;

namespace MainProgram.Utils
{
    public static class SystemInfo
    {
        public static string GetNumberOfCores()
        {
            return new ManagementObjectSearcher("Select * from Win32_Processor")
                .Get()
                .Cast<ManagementBaseObject>()
                .Sum(item => int.Parse(item["NumberOfCores"].ToString()))
                .ToString();
        }

        public static string GetNumberOfLogicalProcessors()
        {
            return Environment.ProcessorCount
                .ToString();
        }
    }
}
