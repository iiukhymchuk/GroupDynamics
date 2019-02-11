using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utilities;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var builder = new StringBuilder();

            builder.AppendLine(textBox1.Text);
            builder.AppendLine(textBox2.Text);
            builder.AppendLine(textBox3.Text);
            builder.AppendLine(textBox4.Text);
            builder.AppendLine(textBox5.Text);

            var text = builder.ToString();

            FileHelpers.SaveFile(text, "Виберіть файл куди зберегти питання");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var questionsText = FileHelpers.ReadFile("Виберіть файл з питаннями");

            var questions = Regex.Split(questionsText, "\r\n|\r|\n");

            if (questions.Length == 0)
                return;

            textBox1.Text = questions.Length > 0 ? questions[0] : "";
            textBox2.Text = questions.Length > 1 ? questions[1] : "";
            textBox3.Text = questions.Length > 2 ? questions[2] : "";
            textBox4.Text = questions.Length > 3 ? questions[3] : "";
            textBox5.Text = questions.Length > 4 ? questions[4] : "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var builder = new StringBuilder();

            builder.AppendLine(textBox6.Text);
            builder.AppendLine(textBox7.Text);
            builder.AppendLine(textBox8.Text);
            builder.AppendLine(textBox9.Text);
            builder.AppendLine(textBox10.Text);

            var text = builder.ToString();

            FileHelpers.SaveFile(text, "Виберіть файл куди зберегти відповіді");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var answerText = FileHelpers.ReadFile("Виберіть файл з відповідями");

            var questions = Regex.Split(answerText, "\r\n|\r|\n");

            if (questions.Length == 0)
                return;

            textBox6.Text = questions.Length > 0 ? questions[0] : "";
            textBox7.Text = questions.Length > 1 ? questions[1] : "";
            textBox8.Text = questions.Length > 2 ? questions[2] : "";
            textBox9.Text = questions.Length > 3 ? questions[3] : "";
            textBox10.Text = questions.Length > 4 ? questions[4] : "";
        }
    }
}
