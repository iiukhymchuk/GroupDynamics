using System.IO;
using System.Windows.Forms;

namespace Utilities
{
    public class FileHelpers
    {
        const string txtFilter = "Text Document|*.txt";

        public static string ReadFile(string titleText)
        {
            using (var openDialog = new OpenFileDialog { Multiselect = false, Title = titleText, Filter = txtFilter })
            {
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openDialog.FileName;

                    using (var file = new FileStream(fileName, FileMode.Open))
                    using (var reader = new StreamReader(file))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;
        }

        public static void SaveFile(string textToSave, string titleText)
        {
            using (var saveDialog = new SaveFileDialog { Title = titleText, Filter = txtFilter })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileToSaveName = saveDialog.FileName;
                    File.WriteAllText(fileToSaveName, textToSave);
                }
            }
        }
    }
}
