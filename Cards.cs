using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Prsi.Properties;
using System.Resources;

namespace Prsi
{
    /// <summary>
    /// Playing card class. Main card's class.
    /// </summary>
    class Card
    {
        protected int suit;
        protected int id;
        protected int val;
        protected int[] inf = new int[2];
        protected string name;

        public Image CardImg;
        public int[] index = new int[2];

        public void SetImg(string img_name, ResourceManager rm)
        {
            if (char.IsNumber(img_name[0])) img_name = "_" + img_name;
            this.CardImg = (Image)rm.GetObject(img_name);
        }

        public void SetInf(int x, int y)
        {
            this.inf[0] = x;
            this.inf[1] = y;
        }

        public int[] ShowInf()
        {
            return this.inf;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string CardName()
        {
            return this.name;
        }

        public int GetCardValue()
        {
            return this.val;
        }

        /// <summary>
        /// Creating new card in player's deck
        /// </summary>
        /// <param name="img_name">Card's name</param>
        /// <returns>
        /// PictureBox for player's flowLayoudPanel
        /// </returns>
        public PictureBox Player_cards(string img_name)
        {
            PictureBox pb = new PictureBox();
            pb.Image = this.CardImg;
            pb.Height = 75;
            pb.Width = 60;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            return pb;
        }
    }

    class Ace : Card
    {
        public Ace(int suit)
        {
            this.id = 0;
            this.suit = suit;
            this.val = 15;
            SetInf(0, 1);
        }
    }

    class Six : Card
    {
        public Six(int suit)
        {
            this.id = 1;
            this.suit = suit;
            this.val = 0;
        }
    }

    class Seven : Card
    {
        public Seven(int suit)
        {
            this.id = 2;
            this.suit = suit;
            this.val = 0;
            SetInf(1, 0);
        }
    }

    class Eight : Card
    {
        public Eight(int suit)
        {
            this.id = 3;
            this.suit = suit;
            this.val = 0;
            SetInf(2, 1);
        }
    }

    class Nine : Card
    {
        public Nine(int suit)
        {
            this.id = 4;
            this.suit = suit;
            this.val = 0;
        }
    }

    class Ten : Card
    {
        public Ten(int suit)
        {
            this.id = 5;
            this.suit = suit;
            this.val = 10;
        }
    }

    class Jack : Card
    {
        public Jack(int suit)
        {
            this.id = 6;
            this.suit = suit;
            this.val = 20;
        }
    }

    class Queen : Card
    {
        public Queen(int suit)
        {
            this.id = 7;
            this.suit = suit;
            if (this.suit == 1)
            {
                this.val = 50;
                SetInf(5, 1);
            }
            else this.val = 10;
        }
    }

    class King : Card
    {
        public King(int suit)
        {
            this.id = 8;
            this.suit = suit;
            if (this.suit == 3)
            {
                this.val = 30;
                SetInf(3, 0);
            }
            else this.val = 10;
        }
    }
}
