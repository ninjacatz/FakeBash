using System;
using System.IO;

namespace FakeBash 
{
    public class ChangeDirectoryClass 
    {
        public static void ChangeDirectory(string inputDirectory)
        {   
            //TODO: figure out recursion (?) for "../../../" (multiple parent directories)
            
            //check for special cases
            switch (inputDirectory)
            {
                case "":
                    //return to homeDirectory
                    Program.currentDirectory = Program.homeDirectory;
                    return;
                case "-":
                    //prints and changes to previousDirectory
                    Console.WriteLine(Program.previousDirectory);
                    string temp = Program.currentDirectory;
                    Program.currentDirectory = Program.previousDirectory;
                    Program.previousDirectory = temp;
                    return;
                case "~":
                    //navigate to homeDirectory
                    Program.currentDirectory = Program.homeDirectory;
                    return;
                case ".":
                    //literally do nothing
                    return;
                case "..":
                    //go to parent directory
                    if (Program.currentDirectory == "/")
                    {
                        return;
                    }
                    Program.currentDirectory = Program.currentDirectory.Substring(0, Program.currentDirectory.LastIndexOf('/'));
                    return;
            }

            //if inputDirectory starts with '/', this means not to add inputDirectory to currentDirectory, but to set inputDirectory as currentDirectory if it exists
            if (inputDirectory.StartsWith('/'))
            {
                if (!Directory.Exists(inputDirectory))
                {
                    Console.WriteLine("-bash: cd: " + inputDirectory + ": No such file or directory");
                }
                else
                {
                    Program.previousDirectory = Program.currentDirectory;

                    Program.currentDirectory = inputDirectory;
                }
            }
            //if inputDirectory starts with "~/", this means to add homeDirectory to inputDirectory if it exists
            else if (inputDirectory.StartsWith("~/"))
            {
                if (!Directory.Exists(Program.homeDirectory + inputDirectory.Substring(1)))
                {
                    Console.WriteLine("-bash: cd: " + inputDirectory.Substring(2) + ": No such file or directory");
                }
                else
                {
                    Program.previousDirectory = Program.currentDirectory;

                    Program.currentDirectory = Program.homeDirectory + inputDirectory.Substring(1);
                }
            }
            //if inputDirectory starts with "../", this means to go to parent directory and then go to what follows if parentDirectory + what follows of inputDirectory exists
            else if (inputDirectory.StartsWith("../"))
            {
                //find parent directory
                string parentDirectory = Program.currentDirectory.Substring(0, Program.currentDirectory.LastIndexOf('/'));

                if (!Directory.Exists(parentDirectory + inputDirectory.Substring(2)))
                {
                    Console.WriteLine("-bash: cd: " + inputDirectory.Substring(3) + ": No such file or directory");
                }
                else
                {
                    Program.previousDirectory = Program.currentDirectory;

                    Program.currentDirectory = parentDirectory + inputDirectory.Substring(2);
                }
            }
            //if inputDirectory does not start with '/', "../", or "~/"
            else 
            {
                //add '/' to start of inputDirectory (needed to add currentDirectory and inputDirectory together)
                inputDirectory = inputDirectory.Insert(0, "/");

                if (!Directory.Exists(Program.currentDirectory + inputDirectory))
                {
                    Console.WriteLine("-bash: cd: " + inputDirectory.Substring(1) + ": No such file or directory");
                }
                else
                {
                    Program.previousDirectory = Program.currentDirectory;

                    //if currentDirectory is root ("/"), do not add currentDirectory and inputDirectory (because this creates "//" problem)
                    if (Program.currentDirectory == "/")
                    {
                        Program.currentDirectory = inputDirectory;
                    }
                    else
                    {
                        Program.currentDirectory = Program.currentDirectory + inputDirectory;
                    }
                }
            }
        }
    }
}