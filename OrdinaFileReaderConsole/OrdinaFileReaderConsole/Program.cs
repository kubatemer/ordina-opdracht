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
                    string extension = Path.GetExtension(path);

                    if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileContent = File.ReadAllText(path);

                        if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            DisplayContent(fileContent);
                            AskForDecryption(fileContent);
                        }
                        else if (extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            HandleXmlFile(path, fileContent);
                        }
                    }
                    else
                    {
                        DisplayError($"Given file (\"{path}\") is not a text or XML file.");
                    }
                }
                else
                {
                    DisplayError($"Given path (\"{path}\") is invalid or empty.");
                }
            }
            catch (Exception)
            {
                DisplayError("Something went wrong.");
            }
            Console.ReadKey();
        }

        static void DisplayContent(string content)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Content:\n\n" + content);
        }

        static void AskForDecryption(string content)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nDo you want to decrypt the file? (y/n): ");
            char response = Console.ReadKey().KeyChar;

            if (response == 'y' || response == 'Y')
            {
                Console.Clear();
                DecryptContent(content);
            }
        }

        static void DecryptContent(string content)
        {
            EncryptionService encryptionService = new EncryptionService();
            encryptionService.SetEncryption(new ReverseEncryption());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Decrypted Content:\n\n" + encryptionService.DecryptText(content));
        }

        static void HandleXmlFile(string path, string content)
        {
            Console.Write("Enter your role (admin/user): ");
            string role = Console.ReadLine();

            RoleSecurity roleSecurity = new RoleSecurity();

            if (roleSecurity.CanReadFile(path, role))
            {
                Console.Clear();
                DisplayContent(content);
                AskForDecryption(content);
            }
            else
            {
                DisplayError($"You do not have permission to read this file as {role}.");
            }
        }

        static void DisplayError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
        }
    }
}
