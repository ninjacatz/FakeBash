using System;
using System.IO;

namespace FakeBash
{
    public class MoveOrRenameClass
    {
        public static void MoveOrRename(string[] inputArray)
        {
            string originalName = inputArray[1];
            string newNameOrDestination = inputArray[2];

            string originalNameFilePath = Program.currentDirectory + "/" + originalName;
            string newNameOrDestinationFilePath = Program.currentDirectory + "/" + newNameOrDestination;

            //backup file name is complicated so it doesn't potentially overwrite any existing files with the same name
            //WARNING: error is NOT thrown if "~!@#$[;]backup[;]~!@#$" already exists and is overwritten
            string backupFilePath = Program.currentDirectory + "/" + "~!@#$[;]backup[;]~!@#$";

            if (File.Exists(originalNameFilePath))
            {
                if (File.Exists(newNameOrDestinationFilePath))
                {
                    //mv file1 file2
                    //file2 already exists
                    //overwrite file2
                    File.Replace(originalNameFilePath, newNameOrDestinationFilePath, backupFilePath);
                    File.Delete(backupFilePath);
                }
                else if (Directory.Exists(newNameOrDestinationFilePath))
                {
                    //mv file1 directory2
                    //directory2 already exists
                    //put file in folder
                    File.Move(originalNameFilePath, newNameOrDestinationFilePath + "/" + originalName);
                }
                else
                {
                    //mv file1 ???
                    //rename file1 to ???
                    File.Move(originalNameFilePath, newNameOrDestinationFilePath);
                }
            }
            else if (Directory.Exists(originalNameFilePath))
            {
                if (File.Exists(newNameOrDestinationFilePath))
                {
                    //mv folder1 file2
                    //file2 already exists
                    Console.WriteLine("mv: rename " + originalName + " to " + newNameOrDestination + ": Not a directory");

                }
                else if (Directory.Exists(newNameOrDestinationFilePath))
                {
                    //mv folder1 folder2
                    //folder2 already exists
                    //put folder1 into folder2
                    Directory.Move(originalNameFilePath, newNameOrDestinationFilePath + "/" + originalName);
                }
                else
                {
                    //mv folder1 ???
                    //rename folder1 to ???
                    Directory.Move(originalNameFilePath, newNameOrDestinationFilePath);
                }
            }
            else
            {
                //file or folder doesn't exist
                Console.WriteLine("mv: " + originalName + " to " + newNameOrDestination + ": No such file or directory");
            }
        }
    }
}