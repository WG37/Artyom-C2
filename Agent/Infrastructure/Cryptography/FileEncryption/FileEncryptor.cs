using AgentClient.Infrastructure.Utilities.KeyGen;
using System.Security.Cryptography;

namespace AgentClient.Infrastructure.Cryptography.FileEncryption
{
    public class FileEncryptor
    {
        private readonly byte[] _key;

        public FileEncryptor(byte[] key)
        {
            _key = key;
        }

        public void EncryptFile(string path)
        {
            var pt = File.ReadAllBytes(path);
            var iv = KeyGeneration.KeyGen(16);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = iv;

            using var ms = new MemoryStream();
            ms.Write(iv, 0, iv.Length);

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(pt, 0, pt.Length);
            }

            File.WriteAllBytes(path, ms.ToArray());
        }
    }
}
