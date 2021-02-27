using System;
using System.IO;
using System.Collections.Generic;

namespace FakeBash
{
    public class ListClass
    {
        public static bool showHidden;
        public static bool showTitle;

        public static void List(string[] inputArray)
        {
            showHidden = false;
            showTitle = false;

            //"ls"
            if (inputArray.Length == 1)
            {
                PrintDirectoryList(Program.currentDirectory);

                return;
            }
            //"ls directory"
            else if (inputArray.Length == 2 & inputArray[1] != "-a")
            {
                DetermineInput(inputArray[1]);

                return;
            }
            //"ls directory1 directory2 etc"
            else if (inputArray.Length > 1 & inputArray[1] != "-a")
            {
                showTitle = true;

                for (int index = 1; index < inputArray.Length; index++)
                {
                    DetermineInput(inputArray[index]);

                    //prints a line unless on last directory/file
                    if (index != inputArray.Length - 1)
                    {
                        Console.WriteLine();
                    }
                }

                return;
            }

            if (inputArray[1] == "-a") 
            {
                showHidden = true;
                
                //"ls -a directory"
                if (inputArray.Length == 3)
                {
                    DetermineInput(inputArray[2]);
                }
                //"ls -a directory1 directory2 etc"
                else if (inputArray.Length > 3)
                {
                    showTitle = true;
                    
                    for (int index = 2; index < inputArray.Length; index++)
                    {
                        DetermineInput(inputArray[index]);

                        //prints a line unless on last directory/file
                        if (index != inputArray.Length - 1)
                        {
                            Console.WriteLine();
                        }
                    }
                }
                else //only "ls -a"
                {
                    PrintDirectoryList(Program.currentDirectory);
                }
            }                   
        }

        static void PrintDirectoryList(string directoryPath)
        {
            //create full list of directories and files
            string[] directories = Directory.GetDirectories(directoryPath);
            string[] files = Directory.GetFiles(directoryPath);
            List<string> allFilesAndDirectories = new List<string>();
            allFilesAndDirectories.InsertRange(0, files);
            allFilesAndDirectories.InsertRange(0, directories);
            string[] finalList;

            //stripping the file/directory names to get rid of the full path
            for (int index = 0; index < allFilesAndDirectories.Count; index++)
            {
                if (directoryPath == "/")
                {
                    allFilesAndDirectories[index] = allFilesAndDirectories[index].Substring(1);
                }
                else
                {
                    allFilesAndDirectories[index] = allFilesAndDirectories[index].Substring(directoryPath.Length + 1);
                }
            }

            //if showHidden false, remove any files that start with "."
            if (!showHidden)
            {
                for (int index = 0; index < allFilesAndDirectories.Count; index++)
                {
                    if (allFilesAndDirectories[index].StartsWith("."))
                    {
                        allFilesAndDirectories.RemoveAt(index);
                        index--;
                    }
                }
            }
            else //sort "." files to front anyway
            {
                for (int index = 0; index < allFilesAndDirectories.Count; index++)
                {
                    if (allFilesAndDirectories[index].StartsWith("."))
                    {
                        string temp = allFilesAndDirectories[index];
                        allFilesAndDirectories.RemoveAt(index);
                        allFilesAndDirectories.Insert(0, temp);
                    }
                }
            }

            //instantiate finalList
            finalList = allFilesAndDirectories.ToArray();

            //finding file or directory name with max length in finalList
            int max = 0;
            foreach (string name in finalList)
            {
                if (name.Length > max)
                {
                    max = name.Length;
                }
            }

            //set nameWidth to longest file/directory name + 2 (to leave room)
            int nameWidth = max + 2;
            //create numberOfColumns based on console window width
            int numberOfColumns = Console.WindowWidth / nameWidth;

            //print title if true
            if (showTitle)
            {
                Console.WriteLine(directoryPath.Substring(directoryPath.LastIndexOf('/') + 1) + ":");
            }

            //loop through each file/directory, printing it and skipping to a new line when a column is full (the reason for columnCounter)
            int columnCounter = 0;
            foreach (string fileName in finalList)
            {
                columnCounter++;
                
                Console.Write(fileName.PadRight(nameWidth));

                if (columnCounter == numberOfColumns)
                {
                    Console.WriteLine();
                    columnCounter = 0;
                }
            }

            //if reached end of row (when columnCounter set to 0 in above loop) and out of files/directories to print, then new line
            if (columnCounter != 0)
            {
                Console.WriteLine();
            }
        }

        static void PrintFileName(string filePath)
        {
            string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);

            Console.WriteLine(fileName);
        }

        //returns usable directory path string OR if input is a file, calls PrintFileName and returns null
        static void DetermineInput(string input)
        {          
            //input starts with "/" meaning it is a full directory name
            if (input.StartsWith("/"))
            {
                if (Directory.Exists(input))
                {
                    PrintDirectoryList(input);
                }
                else if (File.Exists(input))
                {
                    PrintFileName(input);
                }
                else
                {
                    Console.WriteLine("ls: " + input.Substring(input.LastIndexOf('/')) + ": No such file or directory");
                }
            }
            else //append input to currentDirectory
            {
                string directoryPath = Program.currentDirectory + "/" + input;

                if (Directory.Exists(directoryPath))
                {
                    PrintDirectoryList(directoryPath);
                }
                else if (File.Exists(directoryPath))
                {
                    PrintFileName(directoryPath);
                }
                else
                {
                    Console.WriteLine("ls: " + directoryPath.Substring(directoryPath.LastIndexOf('/')) + ": No such file or directory");
                }
            }
        }
    }
}