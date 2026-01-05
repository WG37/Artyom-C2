using System.Security.Cryptography;

namespace AgentClient.Infrastructure.Cryptography.FileDecryption
{
    public class FileDecryptor
    {
        private readonly byte[] _key;

        public FileDecryptor(byte[] key)
        {
            _key = key;
        }

        public void DecryptFile(string path)
        {
            var ctBytes = File.ReadAllBytes(path);

            var iv = new byte[16];
            Buffer.BlockCopy(ctBytes, 0, iv, 0, 16);

            var ctLen = ctBytes.Length - 16;
            var ct = new byte[ctLen];
            Buffer.BlockCopy(ctBytes, 16, ct, 0, ctLen);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = iv;

            using var ms = new MemoryStream(ct);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);

            using var output = new MemoryStream();
            cs.CopyTo(output);

            File.WriteAllBytes(path, output.ToArray());
        }
    }
}
