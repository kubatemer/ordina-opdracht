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

                        if (Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Content:\n\n" + fileContent);
                            Console.Write("\nDo you want to decrypt the file? (y/n): ");
                            char response = Console.ReadKey().KeyChar;

                            if (response == 'y' || response == 'Y')
                            {
                                Console.Clear();
                                EncryptionService encryptionService = new EncryptionService();
                                encryptionService.SetEncryption(new ReverseEncryption());
                                Console.WriteLine("Decrypted Content:\n\n" + encryptionService.DecryptText(fileContent));
                            }
                        }
                        else if (Path.GetExtension(path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Enter your role (admin/user): ");
                            string role = Console.ReadLine();

                            RoleSecurity roleSecurity = new RoleSecurity();

                            if (roleSecurity.CanReadFile(path, role))
                            {
                                Console.Clear();
                                Console.WriteLine("Content:\n\n" + fileContent);
                            }
                            else { Console.WriteLine($"\nError: You do not have permission to read this file as {role}."); }
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
