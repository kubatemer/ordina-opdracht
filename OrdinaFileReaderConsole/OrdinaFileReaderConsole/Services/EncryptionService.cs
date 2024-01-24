using OrdinaFileReaderConsole.Encryption;

namespace OrdinaFileReaderConsole.Services
{
    public class EncryptionService
    {
        private IEncryption encryption;

        public void SetEncryption(IEncryption x)
        {
            encryption = x;
        }

        public string DecryptText(string content)
        {
            if (encryption != null)
            {
                return encryption.Decrypt(content);
            }
            else
            {
                return "Error: No encryption set.";
            }
        }
    }
}
