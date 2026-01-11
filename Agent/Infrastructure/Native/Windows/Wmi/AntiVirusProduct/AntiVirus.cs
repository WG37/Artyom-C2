using System.Management;

namespace AgentClient.Infrastructure.Native.Windows.Wmi.AntiVirusProduct
{
    public static class AntiVirus
    {
        public static List<(string avName, string avPath)> GetAllAntiVirus()
        {
            var results = new List<(string avName, string avPath)>();

            var scope = new ManagementScope(@"\\.\root\SecurityCenter2");
            var query = new ObjectQuery("SELECT * FROM AntiVirusProduct");

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                foreach (var obj in searcher.Get())
                {
                    var avName = obj["displayName"]?.ToString() ?? "";
                    var avPath = obj["pathToSignedProductExe"]?.ToString() ?? "";

                    results.Add((avName, avPath));
                }
            }
            return results;
        }
    }
}
