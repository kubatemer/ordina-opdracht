using System;

namespace OrdinaFileReaderConsole.Encryption
{
    public class ReverseEncryption : IEncryption
    {
        public string Decrypt(string content)
        {
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
