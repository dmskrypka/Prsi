using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prsi.Properties;
using System.Resources;

namespace Prsi
{
    partial class Choose_suit : Form
    {
        public Game GG;
        public Form1 MainForm;

        public Choose_suit(Game G, Form1 MF)
        {
            InitializeComponent();
            GG = G;
            MainForm = MF;
            Image img;
            img = Resources.c;
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            img = Resources.s;
            pictureBox2.Image = img;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;

            img = Resources.d;
            pictureBox3.Image = img;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;

            img = Resources.h;
            pictureBox4.Image = img;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResourceManager rm = Resources.ResourceManager;
            Card c = new Card();
            c.index[0] = 0;

            if (radioButton1.Checked)
            {
                c.SetImg("c", rm);
                c.index[1] = 0;
            }
            else if (radioButton2.Checked)
            {
                c.SetImg("s", rm);
                c.index[1] = 1;
            }
            else if (radioButton3.Checked)
            {
                c.SetImg("d", rm);
                c.index[1] = 2;
            }
            else if (radioButton4.Checked)
            {
                c.SetImg("h", rm);
                c.index[1] = 3;
            }
            MainForm.AddtoDeck(c);
            GG.UsedCards.AddCard(c);
            this.Close();
        }
    }
}
