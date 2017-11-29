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
    partial class End_Form : Form
    {
        public Form1 Main_Form;
        public GameInfo GI;
        public Game oldGame;

        public End_Form(string name,Form1 MF,Game g)
        {
            InitializeComponent();
            button4.Hide();
            oldGame = g;
            Main_Form = MF;
            label1.Text = name;
            label2.Text = Convert.ToString(oldGame.ShowPoints(1));
            label4.Text = Convert.ToString(oldGame.ShowPoints(0));
            label5.ForeColor = Color.DarkBlue;
            label5.Font = new Font(label5.Font.Name, 12, label5.Font.Style);

            if (oldGame.ShowPoints(0) > 125)
            {
                button1.Hide();
                button4.Show();
                label5.Text = "Start new game?";
                label5.Location = new Point(8, 54);
                string winPl = name + " win!";
                MessageBox.Show(winPl);
            }
            else if (oldGame.ShowPoints(1) > 125)
            {
                button1.Hide();
                button4.Show();
                label5.Location = new Point(8, 54);
                label5.Text = "Start new game?";
                MessageBox.Show("PC win!");
            }
            else if(oldGame.ShowPoints(0) > 125 && oldGame.ShowPoints(1) > 125)
            {
                button1.Hide();
                button4.Show();
                label5.Location = new Point(5, 54);
                label5.Text = "Start new game?";
                MessageBox.Show("All players lose!");
            }
        }

        public void NewGI(GameInfo Insert)
        {
            GI = Insert;
        }

        /// <summary>
        /// Button "Show log": Open log information form.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            Main_Form.Hide();
            Log_Form Log = new Log_Form(GI);
            Log.ShowDialog();
        }

        /// <summary>
        /// Button "NO": close application
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Button "YES": start next or new game
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Main_Form.Show();
            Main_Form.StartG(oldGame);
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main_Form.Show();
            this.Hide();
            Main_Form.button1_Click(sender, e);
        }
    }
}
