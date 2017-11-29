using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Prsi.Properties;
using System.Resources;
using System.IO;

namespace Prsi
{
    partial class Form1 : Form
    {
        public Form1(string name)
        {
            ResourceManager rm = Resources.ResourceManager;
            InitializeComponent();
            this.BackColor = Color.WhiteSmoke;
            Player_name(name);
            Image img = Resources.back;
            object obj = (object)img;
            string s = obj.ToString();

            button3.Hide();
            button4.Hide();
            PlayCards.Image = img;
            PlayCards.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayCards.BorderStyle = BorderStyle.FixedSingle;
            PlayCards.Cursor = Cursors.Hand;
            PlayCards.Enabled = false;
            UsedCards.SizeMode = PictureBoxSizeMode.StretchImage;
            UsedCards.BorderStyle = BorderStyle.FixedSingle;
            PlayCards.Invalidate();

            label6.Font = new Font(this.Font, FontStyle.Bold);
            label7.Font = label6.Font;          
            label6.ForeColor = Color.Red;
            label7.ForeColor = Color.Green;

            button3.Enabled = false;
        }
        
        public void AddtoDeck(object card)
        {
            UsedCards.Image = ((Card)card).CardImg;
        }

        /// <summary>
        /// Add new card's image in player's flowLayoutPanel (deck)
        /// </summary>
        /// <param name="card">Object for adding</param>
        /// <param name="player">Player which get card</param>
        public void Add(object card, int player)
        {
            Card c = (Card)card;
            CustomPictureBox cpb = new CustomPictureBox(c.index,card);
            Image img1 = (player==1 ? ((Card)card).CardImg : Resources.back);
            cpb.Image = img1;
            cpb.Height = 75;
            cpb.Width = 60;
            cpb.SizeMode = PictureBoxSizeMode.StretchImage;
            if (player == 1)
            {
                cpb.MouseHover += Cpb_MouseHover;
                cpb.MouseLeave += Cpb_MouseLeave;
                cpb.MouseClick += Cpb_MouseClick;
                flowLayoutPanel1.Controls.Add(cpb);
            }
            else flowLayoutPanel2.Controls.Add(cpb);
        }

        /// <summary>
        /// Remove card from player's flowLayoutPanel
        /// </summary>
        /// <param name="player">Player's id</param>
        /// <param name="CardInd">Removed card index</param>
        public void flpRemove(int player, int[] CardInd)
        {
            switch (player)
            {
                case 0:
                    foreach(Control c in flowLayoutPanel2.Controls)
                    {
                        CustomPictureBox cpb = (CustomPictureBox)c;
                        if (cpb.index == CardInd) flowLayoutPanel2.Controls.Remove(c);
                    }
                    break;
                case 1:
                    foreach (Control c in flowLayoutPanel1.Controls)
                    {
                        CustomPictureBox cpb = (CustomPictureBox)c;
                        if (cpb.index == CardInd) flowLayoutPanel1.Controls.Remove(c);
                    }
                    break;
            }
        }

        /// <summary>
        /// Action: click on player's card.
        /// </summary>
        private void Cpb_MouseClick(object sender, MouseEventArgs e)
        {
            CustomPictureBox cpb = (CustomPictureBox)sender;
            NewG.NullDeck();
            Card used= (Card)NewG.UsedCards.ReturnCard();
            Card c = (Card)cpb.card;
            int index1 = used.index[0];
            int index2 = used.index[1];
            string text = "You can't play this card in this situation.\nFor more information you can press button Rules";
            if (c.index[0] == 7 || c.index[0] == index1 || c.index[1] == index2)
                NewG.PlayingProcess(1, cpb.card);
            else MessageBox.Show(text,"Error");
        }

        /// <summary>
        /// Action: take off mouse from player's card.
        /// </summary>
        private void Cpb_MouseLeave(object sender, EventArgs e)
        {
            CustomPictureBox cpb = (CustomPictureBox)sender;
            cpb.Paint += Cpb_PaintOff;
            cpb.Height =75;
            cpb.Width =60;
            cpb.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Action: choose card for move.
        /// </summary>
        private void Cpb_MouseHover(object sender, EventArgs e)
        {
            CustomPictureBox cpb = (CustomPictureBox)sender;
            cpb.BorderStyle = BorderStyle.FixedSingle;
            cpb.Paint += Cpb_PaintOn;

            int i = 0;
            while (i < 6)
            {
                cpb.Height +=i;
                cpb.Width += i;
                i += 2;
            }
        }

        /// <summary>
        /// Change card in action: choose card
        /// </summary>
        private void Cpb_PaintOn(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.LightSteelBlue, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Change card in action: take off mouse
        /// </summary>
        private void Cpb_PaintOff(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Set player's name getting from Enter_name form.
        /// </summary>
        /// <param name="name">Player's name</param>
        public void Player_name(string name)
        {
            label1.Text = name;
            label7.Text = name;
        }

        public void SetPlayersPoints(int pl0,int pl1)
        {
            label2.Text = Convert.ToString(pl1);
            label4.Text = Convert.ToString(pl0);
        }

        public string ReturnName()
        {
            return label1.Text;
        }

        public void ButtonEnable(bool tmp)
        {
            button2.Enabled = tmp;
        }

        /// <summary>
        /// Button "Rules"
        /// </summary>
        public void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            MyTread Rule = new MyTread(this,button2);
        }

        private Game NewG;

        /// <summary>
        /// Starting new game or next (after previous) game
        /// </summary>
        /// <param name="g">New or previous game information</param>
        public void StartG(Game g)
        {
            Deck NewD = new Deck();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            PlayCards.Controls.Clear();
            UsedCards.Controls.Clear();
            g.GameDeck(label1.Text, NewD.CardDeck(), this);
        }

        public int PlayerPoints;

        public void EndWithBridge(bool show,int points, int p)
        {
            if (show)
            {
                PlayerPoints = points;
                if (p==1) button4.Show();
                else
                {
                    if (NewG.players[0].GetCardsValue() == 0)
                    {
                        string text = "Game ended after PC bridge.";
                        var result = MessageBox.Show(text, "", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                            NewG.EndGame();
                    }
                    else button4.Hide();
                }
            }
            else button4.Hide();
        }

        /// <summary>
        /// Button "New Game"
        /// </summary>
        public void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            PlayCards.Enabled = true;
            NewG = new Game();
            StartG(NewG);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            End_Form ef = new End_Form(this.label1.Text, this,NewG);
            ef.NewGI(NewG.MainInfo);
            ef.ShowDialog();
        }

        /// <summary>
        /// Button "BRIDGE"
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            string text = "You will get " + Convert.ToString(PlayerPoints) + " points after ending game.";
            var result = MessageBox.Show(text, "", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
                NewG.EndGame();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                Environment.Exit(0);
        }

        private void PlayCards_Click(object sender, EventArgs e)
        {
            Card[] arr = NewG.players[1].ReturnArray();
            NewG.NullDeck();
            Card used = (Card)NewG.UsedCards.ReturnCard();
            bool HaveCard = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].index[0] == 7 || arr[i].index[0] == used.index[0] || arr[i].index[1] == used.index[1]) HaveCard = true;
            }
            if (!HaveCard) NewG.TakeCard(NewG.players[1], NewG.MixedCards.ReturnCard(), 1);
            else
            {
                string text = "You can't take card from playing deck.\nFor more information you can press button Rules";
                MessageBox.Show(text,"Error");
            }
        }
    }

    public class CustomPictureBox : PictureBox
    {
        public object card;
        public int[] index;

        public CustomPictureBox(int[] index, object card)
        {
            this.card = card;
            this.index = index;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }

    /// <summary>
    /// Class for showing rules.
    /// </summary>
    class MyTread
    {
        public Thread thrd;
        Form1 MF;
        Button b;

        public MyTread(Form1 MainForm, Button b2)
        {
            thrd = new Thread(this.Run);
            this.MF = MainForm;
            this.b = b2;
            thrd.Start();
        }

        void Run()
        {
            string s = "";
            string Rules = Resources.Rules;
            char tmp='\0';
            bool check = false;
            for(int i = 0; i < Rules.Length; i++)
            {
                if(Rules[i]==92 && i+1<Rules.Length)
                {
                    i++;
                    switch (Rules[i])
                    {
                        case 't':
                            tmp = '\t';
                            break;
                        case 'n':
                            tmp = '\n';
                            break;
                        case 'r':
                            tmp = '\r';
                            break;
                        case 'b':
                            check = true;
                            break;
                    }
                }
                else tmp=Rules[i];
                if (!check) s = s + tmp;
                else
                {
                    check = false;
                    s = s.Remove(s.Length - 1);
                    if(s.LastIndexOf('\r')==s.Length-1) s = s.Remove(s.Length - 1);
                }
            }
            MessageBox.Show(s,"Rules", MessageBoxButtons.OK);
            if (MF.Visible == false) Environment.Exit(0);
            this.b.BeginInvoke(new Action(() => { b.Enabled = true; }));
            this.thrd.Abort();
        }
    }
}
