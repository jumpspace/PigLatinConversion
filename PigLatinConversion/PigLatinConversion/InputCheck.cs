using System.Text.RegularExpressions;

namespace PigLatinConversion
{
    class InputCheck
    {
        public static bool InputValid(string inputText)
        {
            string validChars = @"[a-zA-Z][a-zA-Z ]+$";
            if (Regex.IsMatch(inputText, validChars))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
