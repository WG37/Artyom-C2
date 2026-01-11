using AgentClient.Domain.Models.Agents;
using AgentClient.Infrastructure.Native.Windows.Wmi;
using AgentClient.Infrastructure.Native.Windows.Wmi.AntiVirusProduct;

namespace AgentClient.Application.Commands
{
    public class ListAntiVirus : AgentCommand
    {
        public override string Name => "list-av";

        public override string Execute(AgentTask task)
        {
            var results = new List<string>();
            
            var avList = AntiVirus.GetAllAntiVirus();
            
            if (avList.Count == 0)
            {
                return "No antivirus products detected";
            }

            foreach (var av in avList)
            {
                results.Add($"{av.avName} | {av.avPath}");
            }

            return string.Join("\n", results);
        }
    }
}
