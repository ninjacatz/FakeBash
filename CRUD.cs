using System;
using System.IO;

namespace FakeBash
{
    public class IDisposableCRUDClass
    {
        public static void CreateFile(string filePath)
        {
            using (File.Create(filePath)) {}
        }

        public static void UpdateFile(string filePath, string update)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(update);
            }
        }
    }
}