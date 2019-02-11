namespace Utilities
{
    public class LetterIndex
    {
        static string CyrillicAlphabet { get; } = "АБВГДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЮЯЬабвгдеєжзиіїйклмнопрстуфхцчшщюяь";

        public static int GetIndex(char letter)
        {
            var index = CyrillicAlphabet.IndexOf(letter) + 1;
            return index;
        }

        public static char GetLetter(int index)
        {
            var letter = CyrillicAlphabet[index - 1];
            return letter;
        }
    }
}
