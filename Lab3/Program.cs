using System;
using System.Text;
using Utilities;

namespace Lab3
{
    class Program
    {
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

            string password = FileHelpers.ReadFile("Select file with password");

            if (password is null)
                return;

            var encrypted = Encrypt(textToEncrypt, password);

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(encrypted);

            FileHelpers.SaveFile(encrypted, "Save encripted file");
        }

        static void Decrypt()
        {
            string textToDecrypt = FileHelpers.ReadFile("Select encripted file");

            if (textToDecrypt is null)
                return;

            string password = FileHelpers.ReadFile("Select file with password");

            if (password is null)
                return;

            var decrypted = Decrypt(textToDecrypt, password);

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(decrypted);

            FileHelpers.SaveFile(decrypted, "Save encripted file");
        }

        static string Encrypt(string textToEncrypt, string password)
        {
            StringBuilder result = new StringBuilder(textToEncrypt.Length);
            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                var letter = textToEncrypt[i];
                var passLetter = password[i % password.Length];
                var encriptedLetterIndex = (LetterIndex.GetIndex(letter) + LetterIndex.GetIndex(passLetter)) % 64;
                result.Append(LetterIndex.GetLetter(encriptedLetterIndex));
            }
            return result.ToString();
        }

        static string Decrypt(string textToDecrypt, string password)
        {
            StringBuilder result = new StringBuilder(textToDecrypt.Length);
            for (int i = 0; i < textToDecrypt.Length; i++)
            {
                var letter = textToDecrypt[i];
                var passLetter = password[i % password.Length];
                var encriptedLetterIndex = (64 + LetterIndex.GetIndex(letter) - LetterIndex.GetIndex(passLetter)) % 64;
                result.Append(LetterIndex.GetLetter(encriptedLetterIndex));
            }
            return result.ToString();
        }
    }
}