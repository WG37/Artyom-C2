using AgentClient.Domain.Models.Agents;
using AgentClient.Infrastructure.Utilities.SharpSploit;
using System.ComponentModel;
using System.Diagnostics;

namespace AgentClient.Application.Commands
{
    public class ListProcess : AgentCommand
    {
        public override string Name => "ps";

        public override string Execute(AgentTask task)
        {
            var results = new SharpSploitResultList<ListProcessResult>();

            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                var result = new ListProcessResult
                {
                    ProcessName = process.ProcessName,
                    ProcessPath = GetProcessPath(process),
                    ProcessId = process.Id,
                    SessionId = process.SessionId
                };
                results.Add(result);
            }
            return results.ToString();
        }

        private string GetProcessPath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch (Win32Exception)
            {
                return "-";
            }
            catch (InvalidOperationException)
            {
                return "";
            }
        }
    }

    public sealed class ListProcessResult : SharpSploitResult
    {
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }
        public int ProcessId { get; set; }
        public int SessionId { get; set; }

        protected internal override IList<SharpSploitResultProperty> ResultProperties => new List<SharpSploitResultProperty>
            {
                new SharpSploitResultProperty {Name = nameof(ProcessName), Value = ProcessName},
                new SharpSploitResultProperty {Name = nameof(ProcessPath), Value = ProcessPath},
                new SharpSploitResultProperty {Name = "PID", Value = ProcessId},
                new SharpSploitResultProperty {Name = nameof(SessionId), Value = SessionId }
            };
    }
}
