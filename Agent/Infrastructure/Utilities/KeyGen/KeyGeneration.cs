using System.Security.Cryptography;

namespace AgentClient.Infrastructure.Utilities.KeyGen
{
    public static class KeyGeneration
    {
        public static byte[] KeyGen(int length = 32)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var bytes = new byte[length];

            using (var csprng = RandomNumberGenerator.Create())
            {
                csprng.GetBytes(bytes);
            }

            return bytes;
        }
    }
}
