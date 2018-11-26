using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-d")
                Decript();
            Encript();
        }

        static void Encript()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select file to encript",
                Filter = "Text Document|*.txt"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openDialog.FileName;

                using (var file = new FileStream(fileName, FileMode.Open))
                using (var reader = new StreamReader(file))
                {
                    var contents = reader.ReadToEnd();
                    var encripted = EncriptText(contents);
                    Console.WriteLine(encripted);
                    // save file
                }
            }
        }

        static void Decript()
        {
            var openDialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = "Select encripted file",
                Filter = "Text Document|*.txt"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openDialog.FileName;

                using (var file = new FileStream(fileName, FileMode.Open))
                using (var reader = new StreamReader(file))
                {
                    var contents = reader.ReadToEnd();
                    Console.WriteLine(DecriptText(contents));
                }
            }
        }

        static string DecriptText(string text) => CypherText(text, 1, 3);

        static string EncriptText(string text) => CypherText(text, -1, -3);

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

        static string GetAlphabet(string input)
        {
            if (AlphabetCyrillic.IndexOf(input[0]) != -1)
                return AlphabetCyrillic;
            return AlphabetLatin;
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

        static string AlphabetCyrillic { get; } = "абвгґдеєжзиіїйклмнопрстуфхцшщьюяАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦШЩЬЮЯ";
        static string AlphabetLatin { get; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}