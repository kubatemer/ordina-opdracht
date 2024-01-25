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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ordina file reader");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n------------------------------------------------------------------");
                Console.Write("\nEnter file path or drag and drop the file ('exit' to leave):");
                string path = Console.ReadLine().Replace("\"", "");

                if (path == "exit")
                {
                    Environment.Exit(0);
                }

                try
                {
                    Console.Clear();

                    if (!string.IsNullOrEmpty(path))
                    {
                        string extension = Path.GetExtension(path);

                        if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase) || extension.Equals(".xml", StringComparison.OrdinalIgnoreCase) || extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
                        {
                            string fileContent = File.ReadAllText(path);

                            HandleFile(path, fileContent);
                        }
                        else
                        {
                            DisplayError($"Given file (\"{path}\") is not a text, XML  or JSON file.");
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
            }
        }

        static void HandleFile(string path, string content)
        {
            Console.Write("Enter your role (admin/user): ");
            string role = Console.ReadLine();

            RoleSecurity roleBasedSecurity = new RoleSecurity();

            if (roleBasedSecurity.CanReadFile(path, role))
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

        static void DisplayError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
        }
    }
}
