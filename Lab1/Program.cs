using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab1
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
            using (var openDialog = new OpenFileDialog { Multiselect = false, Title = "Select file to encript", Filter = txtFilter })
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openDialog.FileName;

                    using (var file = new FileStream(fileName, FileMode.Open))
                    using (var reader = new StreamReader(file))
                    {
                        var contents = reader.ReadToEnd();
                        var encripted = EncriyptText(contents);
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine(encripted);
                        using (var saveDialog = new SaveFileDialog { Title = "Save encript file", Filter = txtFilter })
                        {
                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                var fileToSaveName = saveDialog.FileName;
                                File.WriteAllText(fileToSaveName, encripted);
                            }
                        }
                    }
                }
            }
        }

        static void Decrypt()
        {
            using (var openDialog = new OpenFileDialog { Multiselect = false, Title = "Select encripted file", Filter = txtFilter })
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openDialog.FileName;

                    using (var file = new FileStream(fileName, FileMode.Open))
                    using (var reader = new StreamReader(file))
                    {
                        var contents = reader.ReadToEnd();
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine(DecryptText(contents));
                    }
                }
            }
        }

        static string DecryptText(string text) => CypherText(text, 1, 3);

        static string EncriyptText(string text) => CypherText(text, -1, -3);

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
            if (AlphabetCyrillic.IndexOf(input[0]) != -1)
                return AlphabetCyrillic;
            return AlphabetLatin;
        }

        static string AlphabetCyrillic { get; } = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
        static string AlphabetLatin { get; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}