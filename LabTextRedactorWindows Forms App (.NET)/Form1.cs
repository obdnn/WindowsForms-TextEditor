using System;
using System.IO;
using System.Windows.Forms;

namespace LabTextRedactorWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private string currentFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        // NEW
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            currentFilePath = "";
            this.Text = "New File - Text Editor";
        }

        // OPEN
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                textBox1.Text = File.ReadAllText(currentFilePath);
                this.Text = currentFilePath + " - Text Editor";
            }
        }

        // SAVE
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
                else return;
            }
            File.WriteAllText(currentFilePath, textBox1.Text);
            this.Text = currentFilePath + " - Text Editor";
            MessageBox.Show("File saved successfully!");
        }

        // EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // FONT SETTINGS — открывает WPF-окно
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentFont = textBox1.Font.Name;
            var currentSize = (double)textBox1.Font.Size;
            var bold        = textBox1.Font.Bold;
            var italic      = textBox1.Font.Italic;

            var wpfWindow = new FontSettingsWindow(currentFont, currentSize, bold, italic);
            if (wpfWindow.ShowDialog() == true)
            {
                var style = System.Drawing.FontStyle.Regular;
                if (wpfWindow.IsBold)   style |= System.Drawing.FontStyle.Bold;
                if (wpfWindow.IsItalic) style |= System.Drawing.FontStyle.Italic;

                textBox1.Font = new System.Drawing.Font(
                    wpfWindow.SelectedFont,
                    (float)wpfWindow.SelectedSize,
                    style
                );
            }
        }
    }
}
