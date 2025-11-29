using AgentClient.Domain.Models.Agents;
using System.Security.Principal;


namespace AgentClient.Application.Commands
{
    public class Whoami : AgentCommand
    {
        public override string Name => "whoami";

        public override string Execute(AgentTask task)
        {
            var identity = WindowsIdentity.GetCurrent();
            return identity.Name;
        }
    }
}
