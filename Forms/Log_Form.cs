using System;
using System.Windows.Forms;
using System.IO;
using Prsi.Objects;

namespace Prsi.Forms
{
    public partial class Log_Form : Form
    {
        private string log="";
        public Log_Form(GameInfo GI)
        {
            InitializeComponent();
            GameInfo Start = GI.root;
            while (Start != null)
            {
                string s;
                Card C = (Card)Start.card;
                s = Convert.ToString(Start.id) + ". " + Start.PlayerName + ": " + C.CardName();
                listBox1.Items.Add(s);
                log =log + s + '\n';
                Start=Start.next;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Saving log file";
            sfd.FileName = "log";
            sfd.DefaultExt = "txt";
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(log);
                sw.Close();
                this.Close();
            }
        }
    }
}
