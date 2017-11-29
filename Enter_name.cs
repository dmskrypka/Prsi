using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prsi
{
    partial class Enter_name : Form
    {
        public Enter_name()
        {
            InitializeComponent();
            textBox1.Text = "Write your name and press OK";
            textBox1.ForeColor = Color.Gray;
            textBox1.TabIndex = 1;
            button1.TabIndex = 0;

            textBox1.GotFocus += RemoveText;
            textBox1.LostFocus += AddText;
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Write your name and press OK";
                textBox1.ForeColor = Color.Gray;
            }
        }

        public void RemoveText(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        public Form1 Main_Form;

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            string s;
            if (textBox1.Text == "" || textBox1.Text == "Write your name and press OK") s = "Player";
            else s=textBox1.Text;
            MessageBox.Show("Your name will be " + s);
            Main_Form = new Form1(s);
            Main_Form.ShowDialog();
            Close();
        }
    }
}
