using System;
using System.Linq;
using Utilities;

namespace Lab5
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string text = FileHelpers.ReadFile("Select file to count symbols");

            var allSymbols = text.Length;
            var numberOfLetters = text.Count(char.IsLetter);
            var numberOfNonLetters = allSymbols - numberOfLetters;

            var textToSave = $"Number of all symbols is {allSymbols}, letters - {numberOfLetters}, non letters - {numberOfNonLetters}.";

            Console.WriteLine(textToSave);

            FileHelpers.SaveFile(textToSave, "Save result file");
        }
    }
}