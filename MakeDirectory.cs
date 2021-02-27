using System;
using System.IO;

namespace FakeBash
{
    public class MakeDirectoryClass
    {
       public static void MakeDirectory(string[] inputArray)
       {
           string directoryPath = Program.currentDirectory + "/" + inputArray[1];
           
           if (!Directory.Exists(directoryPath))
           {
               Directory.CreateDirectory(directoryPath);
           }
           else
           {
               Console.WriteLine("mkdir: " + inputArray[1] + ": File exists");
           }
       }
    }
}