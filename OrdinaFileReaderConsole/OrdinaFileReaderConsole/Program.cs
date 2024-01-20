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
                string path = Console.ReadLine();
                string fileContent = File.ReadAllText(path);
                
                if (path.EndsWith("txt"))
                {
                    Console.Clear();
                    Console.WriteLine("Content:\n\n" + fileContent);
                }
                else
                {
                    Console.WriteLine("\n\nError: Given file is not a text file.");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Given path is empty or invalid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
