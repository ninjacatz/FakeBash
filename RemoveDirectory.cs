using System;
using System.IO;

namespace FakeBash
{
    public class RemoveDirectoryClass
    {
        public static void RemoveDirectory(string[] inputArray)
        {
            string directoryPath = Program.currentDirectory + "/" + inputArray[1];
            
            if (Directory.Exists(directoryPath))
            {
                if (Directory.GetFiles(directoryPath).Length + Directory.GetDirectories(directoryPath).Length == 0)
                {
                    Directory.Delete(directoryPath);
                }
                else
                {
                    Console.WriteLine("rmdir: " + inputArray[1] + ": Directory not empty");
                }
            }
            else
            {
                Console.WriteLine("rmdir: " + inputArray[1] + ": No such directory");
            }
        }
    }
}