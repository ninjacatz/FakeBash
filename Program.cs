using System;

namespace FakeBash
{
    public class Program
    {
        public static string homeDirectory;
        public static string rootDirectory = "/";
        
        public static string currentDirectory;
        public static string previousDirectory;
        public static string displayDirectory;
        
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to FakeBASH, an emulator for the BASH CLI created with C#");

            homeDirectory = FindOSHomeDirectory();
            currentDirectory = homeDirectory;
            previousDirectory = homeDirectory;
            displayDirectory = homeDirectory;

            MainLoop();
        }

        static string FindOSHomeDirectory()
        {
            if (Environment.OSVersion.Platform == PlatformID.MacOSX | Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return Environment.GetEnvironmentVariable("HOME");
            }
            else
            {
                return Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            }
        }

        static void MainLoop()
        {
            for (;;)
            {
                displayDirectory = currentDirectory;

                //if currentDirectory ends with a forward slash then delete it, but ONLY if currentDirectory isn't just a forward slash
                if (currentDirectory.EndsWith('/') & currentDirectory != "/")
                {
                    currentDirectory = currentDirectory.Remove(currentDirectory.Length - 1);
                }

                //if currentDirectory is within homeDirectory, use "~"
                if (currentDirectory.StartsWith(homeDirectory))
                {
                    displayDirectory = "~" + displayDirectory.Substring(homeDirectory.Length);
                }
                
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(Environment.UserName);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("@");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(Environment.MachineName);
                Console.Write(":");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(displayDirectory);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("$ ");

                string input = Console.ReadLine().Trim();

                DetermineInput(input);
            }
        }

        static void DetermineInput(string input)
        {
            string[] inputArray = SplitClass.Split(input);
            
            switch (inputArray[0])
            {
                case "":
                    //do nothing
                    break;
                case "cd":
                    //call ChangeDirectory function. if no input after "cd" given, use empty string
                    if (inputArray.Length == 1)
                    {
                        ChangeDirectoryClass.ChangeDirectory("");
                    }
                    else
                    {
                        ChangeDirectoryClass.ChangeDirectory(inputArray[1]);
                    }
                    break;
                case "pwd":
                    //print currentDirectory
                    Console.WriteLine(currentDirectory);
                    break;
                case "ls":
                    //list contents of currentDirectory
                    ListClass.List(inputArray);
                    break;
                case "echo":
                    //echo input following command
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        EchoClass.Echo(inputArray);
                    }
                    break;
                case "touch":
                    //create new file
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine("usage: touch file");
                    }
                    else
                    {
                        TouchClass.Touch(inputArray);
                    }
                    break;
                case "mkdir":
                    //create new directory
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine("usage: mkdir directory");
                    }
                    else
                    {
                        MakeDirectoryClass.MakeDirectory(inputArray);
                    }
                    break;
                case "grep":
                    //search file for search term
                    if (inputArray.Length <= 2)
                    {
                        Console.WriteLine("usage: grep 'search term' file");
                    }
                    else
                    {
                        GrepClass.Grep(inputArray);
                    }
                    break;
                case "mv":
                    //move or rename
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine("usage:\nmv source target\nmv source directory");
                    }
                    else
                    {
                        MoveOrRenameClass.MoveOrRename(inputArray);
                    }
                    break;
                case "rmdir":
                    //remove directory
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine("usage: rmdir directory");
                    }
                    else
                    {
                        RemoveDirectoryClass.RemoveDirectory(inputArray);
                    }
                    break;
                case "rm":
                    //remove file
                    if (inputArray.Length == 1)
                    {
                        Console.WriteLine("usage: rm file");
                    }
                    else
                    {
                        RemoveFileClass.RemoveFile(inputArray);
                    }
                    break;
                default:
                    //command not found error message
                    Console.WriteLine("-bash: " + inputArray[0] + ": command not found");
                    break;
            }
        }
    }
}
