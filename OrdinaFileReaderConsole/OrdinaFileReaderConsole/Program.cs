using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdinaFileReaderConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ordina file reader v1.0");
            Console.Write("\nPlease enter the path of the text file or drag and drop the file here:");

            try
            {
                string path = Console.ReadLine().Replace("\"","");
                string fileContent = File.ReadAllText(path);
                
                if (Path.GetExtension(path).Equals(".txt", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Content:\n\n" + fileContent);
                }
                else
                {
                    Console.WriteLine("\n\nError: Given file is not a text or XML file.");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Given path is empty or invalid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
            }

            Console.ReadKey();
        }
    }
}
