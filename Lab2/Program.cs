using System;
using System.Text;
using Utilities;

namespace Lab2
{
    class Program
    {
        const string txtFilter = "Text Document|*.txt";

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-d")
                Decrypt();
            else
                Encrypt();
        }

        static void Encrypt()
        {
            string textToEncrypt = FileHelpers.ReadFile("Select file to encript");

            if (textToEncrypt is null)
                return;

            var encrypted = EncryptText(textToEncrypt);

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(encrypted);

            FileHelpers.SaveFile(encrypted, "Save encripted file");
        }

        static void Decrypt()
        {
            string textToDecrypt = FileHelpers.ReadFile("Select encripted file");

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(DecryptText(textToDecrypt));
        }

        static string DecryptText(string text) => CypherText(text, 1, 3);

        static string EncryptText(string text) => CypherText(text, -1, -3);

        static string CypherText(string text, int shift, int maxLength)
        {
            var initialShiftValue = shift;
            var resultText = new StringBuilder(text.Length);
            var alphabet = GetAlphabet(text);
            foreach (var letter in text)
            {
                resultText.Append(GetLetterWithShift(letter, alphabet, shift));
                shift = shift == maxLength ? initialShiftValue : shift + initialShiftValue;
            }
            return resultText.ToString();
        }

        static char GetLetterWithShift(char c, string alphabet, int shift)
        {
            var encriptedIndex = alphabet.IndexOf(c);
            if (encriptedIndex == -1)
            {
                return '?'; // unknown char
            }
            var decriptedIndex = (encriptedIndex - shift) % alphabet.Length;
            if (decriptedIndex < 0)
                decriptedIndex = alphabet.Length + decriptedIndex;
            return alphabet[decriptedIndex];
        }

        static string GetAlphabet(string input)
        {
            if (Globals.CyrillicAlphabet.IndexOf(input[0]) != -1)
                return Globals.CyrillicAlphabet;
            return Globals.LatinAlphabet;
        }
    }
}