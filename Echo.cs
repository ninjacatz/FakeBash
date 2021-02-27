using System;
using System.IO;

namespace FakeBash
{
    class EchoClass
    {     
        //NOTE: this behaves slightly different from the real thing
        //example: 
        //echo 'text to add' >> newfile.txt 'add more' >> existingfile.txt
        //real BASH: appends "text to add add more" to existingfile.txt and creates newfile.txt if it doesnt already exist
        //fake BASH: appends "text to add" to newfile.txt and appends "add more" to existingfile.txt

        public static bool[] appendCommandArray;
        public static bool[] replaceCommandArray;
        public static string[] inputArray;
        
        public static void Echo(string[] internalInputArray)
        {
            bool[] internalAppendCommandArray = new bool[internalInputArray.Length];
            bool[] internalReplaceCommandArray = new bool[internalInputArray.Length];
            bool foundCommand = false;

            //record indices of commands if there are any
            for (int index = 0; index < internalInputArray.Length; index++)
            {
                switch (internalInputArray[index])
                {
                    case ">>":
                        internalAppendCommandArray[index] = true;
                        foundCommand = true;
                        break;
                    case ">":
                        internalReplaceCommandArray[index] = true;
                        foundCommand = true;
                        break;
                }
            }

            //set variables internal to Echo method to class-level variables for use in other methods without needing so many parameters
            appendCommandArray = internalAppendCommandArray;
            replaceCommandArray = internalReplaceCommandArray;
            inputArray = internalInputArray;

            //if found no command at all, simply use default echo behavior
            if (!foundCommand)
            {
                DefaultBehavior();
            }
            else //commands were found
            {
                bool foundCommandError = CheckForInputErrors();

                if (foundCommandError) return;

                for (int index = 0; index < inputArray.Length; index++)
                {
                    if (appendCommandArray[index])
                    {
                        AppendOrReplace(index, ">>");
                    }
                    else if (replaceCommandArray[index])
                    {
                        AppendOrReplace(index, ">");
                    }
                }
            }
        }

        static bool CheckForInputErrors()
        {
            for (int index = 0; index < inputArray.Length; index++)
            {
                //if command exists at index
                if (appendCommandArray[index] | replaceCommandArray[index])
                {
                    //error if nothing comes after command
                    if (index + 1 == inputArray.Length)
                    {
                        Console.WriteLine("-bash: syntax error near unexpected token `newline'");
                        return true;
                    }
                    //error if ">>" comes after command
                    if (appendCommandArray[index + 1])
                    {
                        Console.WriteLine("-bash: syntax error near unexpected token `>>'");
                        return true;
                    }
                    //error if ">" comes after command
                    if (replaceCommandArray[index + 1])
                    {
                        Console.WriteLine("-bash: syntax error near unexpected token `>'");
                        return true;
                    }
                }
            }

            return false;
        }

        static void AppendOrReplace(int indexOfCommand, string command)
        {
            string afterCommandFileName = inputArray[indexOfCommand + 1];
            string filePath = Program.currentDirectory + "/" + afterCommandFileName;
            string beforeCommandInput = "";

            //generate beforeCommandInput
            int distanceToLastCommandOrEcho = FindDistanceToLastCommandOrEcho(indexOfCommand);
            int indexOfLastCommandOrEcho = indexOfCommand - distanceToLastCommandOrEcho;
            for (int index = indexOfLastCommandOrEcho + 1; index < indexOfCommand; index++)
            {
                beforeCommandInput += inputArray[index] + " ";
            }
            beforeCommandInput = beforeCommandInput.Trim();


            if (File.Exists(filePath))
            {
                //append or replace depending on command
                if (command == ">>")
                {
                    IDisposableCRUDClass.UpdateFile(filePath, beforeCommandInput);
                }
                else if (command == ">")
                {
                    IDisposableCRUDClass.CreateFile(filePath);
                    IDisposableCRUDClass.UpdateFile(filePath, beforeCommandInput);
                }
            }
            else //if file doesnt exist
            {
                //create file, then append (no need to replace)
                IDisposableCRUDClass.CreateFile(filePath);
                IDisposableCRUDClass.UpdateFile(filePath, beforeCommandInput);
            }
        }

        static int FindDistanceToLastCommandOrEcho(int indexOfCommand)
        {
            int distanceCounter = 1;

            for (int index = indexOfCommand - 1; index > 0; index--)
            {
                if (appendCommandArray[index] | replaceCommandArray[index])
                {
                    //decrementing so filename after > or >> command isnt counted
                    distanceCounter--;
                    break;
                }

                distanceCounter++;
            }
            
            return distanceCounter;
        }

        static void DefaultBehavior()
        {
            inputArray[0] = null;
            string echoString = String.Join(" ", inputArray);
            Console.WriteLine(echoString.Substring(1));
        }
    }
}