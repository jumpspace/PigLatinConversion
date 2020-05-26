using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PigLatinConversion
{
    /// <summary>
    /// A Pig Latin class that converts plain English words into Pig Latin
    /// </summary>
    class PigLatin
    {
        private static readonly string[] consonantGroup2 = { 
            "Bl", "bl", "Br", "br", "Cl", "cl", "Ch", "ch", "Cr", "cr", "Dr", "dr", "Fl", "fl", 
            "Fr", "fr", "Gl", "gl", "Gr", "gr", "Pl", "pl", "Pr", "pr", "Qu", "qu", "Sc", "sc",
            "Sh", "sh", "Sk", "sk", "Sl", "sl", "Sm", "sm", "Sn", "sn", "Sp", "sp", "St", "st",
            "Sw", "sw", "Th", "th", "Tr", "tr", "Tw", "tw", "Wh", "wh", "Wr", "wr" };
        
        private static readonly string[] consonantGroup3 = { "Sch", "sch", "Scr", "scr", "Shr", 
            "shr", "Sph", "sph", "Spl", "spl", "Spr", "spr", "Squ", "squ", "Str", "str", "Thr", 
            "thr" };

        private static readonly string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", 
            "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z", "B", "C", "D", "F", 
             "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z" };

        private static readonly string[] vowels = { "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
        private static readonly string[] restricted = { "A", "a", "I" };

        /// <summary>
        /// Create the regular expression to search for consonants
        /// </summary>
        /// <returns>the regular expression to search for consonants</returns>
        private static string BuildConsonantRegEx()
        {
            StringBuilder conso = new StringBuilder("^");
            conso.Append("[");
            foreach (string consonant in consonants)
            {
                conso.Append(consonant);
            }
            conso.Append("]");

            return conso.ToString();
        }

        /// <summary>
        /// Creat the regular expression to serach for consonant groups
        /// </summary>
        /// <returns>the regular expression to search for consonant groups</returns>
        private static string BuildConsonantGroupRegEx()
        {
            int counter;

            StringBuilder consoGrp = new StringBuilder("^(");
            consoGrp.Append(consonantGroup2[0]);
            for (counter = 1; counter < consonantGroup2.Length; counter++)
            {
                consoGrp.Append("|");
                consoGrp.Append(consonantGroup2[counter]);
            }

            foreach (string consoGrp3 in consonantGroup3)
            {
                consoGrp.Append("|");
                consoGrp.Append(consoGrp3);
            }

            consoGrp.Append(")");
            return consoGrp.ToString();
        }

        /// <summary>
        /// Create the regular expression to search for vowels
        /// </summary>
        /// <returns>the regular expression to search for vowels</returns>
        private static string BuildVowelRegEx()
        {
            StringBuilder vowelGrp = new StringBuilder("^");
            vowelGrp.Append("[");
            foreach (string vowel in vowels)
            {
                vowelGrp.Append(vowel);
            }
            vowelGrp.Append("]");

            return vowelGrp.ToString();
        }

        /// <summary>
        /// Create the regular expression to search for restricted "words"
        /// </summary>
        /// <returns>the regular expression to search for restricted "words"</returns>
        private static string BuildRestrictedRegEx()
        {
            StringBuilder restrictedGrp = new StringBuilder("^");
            restrictedGrp.Append("[");
            foreach (string restrictedLetter in restricted)
            {
                restrictedGrp.Append(restrictedLetter);
            }
            restrictedGrp.Append("]$");

            return restrictedGrp.ToString();
        }

        /// <summary>
        /// Determine whether a letter in a word is a vowel
        /// </summary>
        /// <param name="letter">the letter to check. Must be of type string</param>
        /// <returns>boolean value true|false</returns>
        private static bool IsVowel(char letter)
        {
            string letterToCheck = letter + "";
            for (int counter = 0; counter < vowels.Length - 1; counter++)
            {
                if (letterToCheck == vowels[counter])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Main function to convert a sentence into Pig Latin
        /// </summary>
        /// <param name="text">the text to conver to Pig Latin</param>
        /// <returns>the converted Pig Latin text</returns>
        public static string Convert(string text)
        {
            string wordToConvert;
            int charPtr;
             
            int lastPtr = 0;
            string convertedWord = "";
            StringBuilder convertedText = new StringBuilder();


            while ((charPtr = text.IndexOf(" ", lastPtr)) != -1)
            {
                wordToConvert = text.Substring(lastPtr, charPtr - lastPtr);
                convertedWord = ConvertWord(wordToConvert);


                convertedText.Append(convertedWord);
                lastPtr = charPtr + 1;                  // Mark the position of the just-converted word.
            }

            wordToConvert = text.Substring(lastPtr, (text.Length - lastPtr));    // last word to convert
            convertedWord = ConvertWord(wordToConvert);

            convertedText.Append(convertedWord);

            return convertedText.ToString();
        }

        /// <summary>
        /// Convert a single word to Pig Latin
        /// </summary>
        /// <param name="wordToConvert"></param>
        /// <returns>The Pig Latin-converted word</returns>
        private static string ConvertWord(string wordToConvert)
        {
            char letterToMove;
            bool match;

            string convertedWord = "";
            string partToMove = "";
            string partialWord = "";
            Regex vowelsList = new Regex(BuildVowelRegEx());
            Regex consonantsList = new Regex(BuildConsonantRegEx());
            Regex consoGroupsList = new Regex(BuildConsonantGroupRegEx());
            Regex restrictedList = new Regex(BuildRestrictedRegEx());

            match = false;
            if (restrictedList.Match(wordToConvert).Success)
            {
                convertedWord = wordToConvert + " ";
            }
            else if (vowelsList.Match(wordToConvert).Success)
            {
                convertedWord = wordToConvert + "yay ";
            }
            else if (consoGroupsList.Match(wordToConvert).Success)
            {
                if (IsVowel(wordToConvert[2]))    // if the 3rd letter is a vowel, it's a 2-letter consonant group
                {
                    for (int counter = 0; counter < consonantGroup2.Length && !match; counter++)
                    {
                        partToMove = wordToConvert.Substring(0, 2);
                        if (partToMove == consonantGroup2[counter])
                        {
                            partialWord = wordToConvert.Substring(2, wordToConvert.Length - 2);
                            match = true;
                        }
                    }
                }
                else    // it's a 3-letter consonant group
                {
                    for (int counter = 0; counter < consonantGroup3.Length && !match; counter++)
                    {
                        partToMove = wordToConvert.Substring(0, 3);
                        if (partToMove == consonantGroup3[counter])
                        {
                            partialWord = wordToConvert.Substring(3, wordToConvert.Length - 3);
                            match = true;
                        }
                    }
                }
                convertedWord = partialWord + partToMove + "ay ";
            }
            else if (consonantsList.Match(wordToConvert).Success)
            {
                letterToMove = wordToConvert[0];
                partialWord = wordToConvert.Substring(1, wordToConvert.Length - 1);
                convertedWord = partialWord + letterToMove + "ay ";
            }

            return convertedWord;
        }
    }
}
