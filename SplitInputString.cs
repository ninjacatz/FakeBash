using System.Linq;
using System.Text.RegularExpressions;

namespace FakeBash
{
    public class SplitClass
    {
        public static string[] Split(string input)
        {
            //splits directory string using "\ " as an escape sequence
            //ignores spaces in between single quotes

            //TODO: ignore double quotes
            
            input = input.Replace(@"\ ", "{{SPACE}}");

            string[] splitString = Regex.Split(input, " (?=(?:[^']*'[^']*')*[^']*$)").Select(stringItem => stringItem.Replace("{{SPACE}}", " ").Replace("'", "")).ToArray();

            return splitString;
        }
    }
}