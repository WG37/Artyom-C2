using System.Management;

namespace AgentClient.Infrastructure.Utilities.HWIDs.ComponentIDs
{
    public class SystemUuid
    {
        public static string GetSystemUuid()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct"))
            {
                foreach (var obj in searcher.Get())
                {
                    var uuid = obj["UUID"]?.ToString() ?? "";

                    return uuid;
                }
            }

            return "";
        }
    }
}
