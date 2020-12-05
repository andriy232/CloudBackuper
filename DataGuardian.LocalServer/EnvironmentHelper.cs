using System;
using System.Management;
using System.Text;

namespace DataGuardian.LocalServer
{
    public class EnvironmentHelper
    {
        public static string GetUniqueComputerId()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(GetCpuName() + GetUserName()));
        }

        private static string GetCpuName()
        {
            using (var mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor"))
            {
                var collection = mos.Get();
                foreach (var o in collection)
                {
                    var mo = (ManagementObject) o;
                    return mo["Name"].ToString()?.Trim();
                }
            }

            return string.Empty;
        }

        private static string GetUserName()
        {
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }
    }
}