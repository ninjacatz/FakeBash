using System.IO;

namespace FakeBash
{
    using System;

    public class TouchClass
    {
        public static void Touch(string[] inputArray)
        {
            string filePath = "";

            for (int index = 1; index < inputArray.Length; index++)
            {
                filePath = Program.currentDirectory + "/" + inputArray[index];

                if (!File.Exists(filePath))
                {
                    IDisposableCRUDClass.CreateFile(filePath);
                }
                else
                {
                    Console.WriteLine("touch: " + inputArray[index] + ": File already exists");
                }
            }
        }
    }
}