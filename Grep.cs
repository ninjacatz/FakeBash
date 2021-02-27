using System;
using System.IO;

namespace FakeBash
{
    public class GrepClass
    {
        //TODO: grep an entire directory (requires "-r")

        public static void Grep(string[] inputArray)
        {
            string searchTerm = inputArray[1];
            string fileToSearch = inputArray[2];
            string filePath = Program.currentDirectory + "/" + fileToSearch;
            
            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    if (line.Contains(searchTerm))
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("grep: " + fileToSearch + ": No such file");
            }
        }
    }
}

