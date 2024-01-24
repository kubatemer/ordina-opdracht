using OrdinaFileReaderConsole.Encryption;
using OrdinaFileReaderConsole.Services;
using System;
using System.IO;

namespace OrdinaFileReaderConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ordina file reader");
            Console.Write("\nPlease enter the path of the file or drag and drop the file here:");

            try
            {
                string path = Console.ReadLine().Replace("\"", "");

                Console.Clear();

                if (!string.IsNullOrEmpty(path))
                {
                    if (Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileContent = File.ReadAllText(path);
                        Console.WriteLine("Content:\n\n" + fileContent);

                        if (Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("\nDo you want to decrypt the file? (y/n): ");
                            char response = Console.ReadKey().KeyChar;

                            if (response == 'y' || response == 'Y')
                            {
                                Console.Clear();
                                var encryptionService = new EncryptionService();
                                encryptionService.SetEncryption(new ReverseEncryption());
                                Console.WriteLine("Decrypted Content:\n\n" + encryptionService.DecryptText(fileContent));
                            }
                        }
                    }
                    else { Console.WriteLine($"Error: Given file (\"{path}\") is not a text or XML file."); }
                }
                else { Console.WriteLine($"Error: Given path (\"{path}\") is invalid"); }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Something went wrong.");
            }

            Console.ReadKey();
        }
    }
}
