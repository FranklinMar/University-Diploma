using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Diploma
{
    public static class Prompt
    {
        public static string ShowDialog(string Text, string Caption)
        {
            System.Drawing.Font Font = new("Roboto", 10);
            System.Drawing.Color White = System.Drawing.Color.White;
            System.Drawing.Color Black = System.Drawing.Color.Black;
            Form Prompt = new()
            {
                Width = 300,
                Height = 86,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = Caption,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = Black,
            };
            Label TextLabel = new() { /*Left = 50, Top = 20,*/ Text = Text, Width = 300, Font = Font, ForeColor = White };
            TextBox TextBox = new() { /*Left = 50, Top = 50, */Width = 284, BackColor = Black, Font = Font, ForeColor = White };
            Button Confirmation = new() { Text = "OK", /*Left = 350, Top = 70,*/ Left = 100, Top = 24, Width = 100, 
                DialogResult = DialogResult.OK, Font = Font, ForeColor = White };
            Confirmation.Click += (sender, e) => {
                if (string.IsNullOrEmpty(TextBox.Text.Trim()))
                {
                    MessageBox.Show("Cannot have empty string", "Error Message");
                    Prompt.Show();
                } else
                {
                    Prompt.Close();
                }
            };
            Prompt.Controls.Add(TextBox);
            Prompt.Controls.Add(Confirmation);
            Prompt.Controls.Add(TextLabel);
            Prompt.AcceptButton = Confirmation;
            Prompt.ShowDialog();
            //if ()
            if (Prompt.DialogResult == DialogResult.Cancel)
            {
                return null;
            } 
            return TextBox.Text;
            //return Prompt.ShowDialog() == DialogResult.OK ? TextBox.Text : throw new Exception("Cannot have empty name");
        }
    }
}
