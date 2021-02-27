using System;
using System.IO;

namespace FakeBash
{
    public class RemoveFileClass
    {
        public static void RemoveFile(string[] inputArray)
        {
            string filePath = "";

            for (int index = 1; index < inputArray.Length; index++)
            {
                filePath = Program.currentDirectory + "/" + inputArray[index];
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else if (Directory.Exists(filePath))
                {
                    Console.WriteLine("rm: " + inputArray[index] + ": is a directory");
                }
                else
                {
                    Console.WriteLine("rm: " + inputArray[index] + ": No such file");
                }
            }
        }
    }
}