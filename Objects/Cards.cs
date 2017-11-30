using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Prsi.Properties;
using System.Resources;

namespace Prsi.Objects
{
    class CardFactory
    {
        public CardFactory() { }
        public ICard GetCard(int Type,int Suit)
        {
            switch (Type)
            {
                case 1:
                    return new Ace(Suit);
                case 2:
                    return new Six(Suit);
                case 3:
                    return new Seven(Suit);
                case 4:
                    return new Eight(Suit);
                case 5:
                    return new Nine(Suit);
                case 6:
                    return new Ten(Suit);
                case 7:
                    return new Jack(Suit);
                case 8:
                    return new Queen(Suit);
                case 9:
                    return new King(Suit);
                default:
                    return null;
            }
        }
    }

    public enum CardType
    {
        Ace,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public enum CardSuit
    {
        Clubs,
        Spades,
        Diamonds,
        Hearts
    }
    interface ICard
    {
        int Value { get; }
        CardType TypeOfCard { get; }
        CardSuit SuitOfCard { get; }
        PictureBox CardImg { get; }

        string GetCardName();
    }

    abstract class Card
    {
        ResourceManager rm = Resources.ResourceManager;

        public int Value { get; set; }
        public CardType TypeOfCard { get; set; }
        public CardSuit SuitOfCard { get; set; }
        public PictureBox CardImg { get; set; }

        protected void SetImg()
        {
            Image cImg = (Image)rm.GetObject(String.Format(this.TypeOfCard.ToString() + "" + this.SuitOfCard.ToString()));
            this.CardImg = SpecCardImg(cImg);
        }

        public string GetCardName()
        {
            return String.Format(this.TypeOfCard.ToString()+" of "+this.SuitOfCard.ToString());
        }

        PictureBox SpecCardImg(Image cImg)
        {
            PictureBox pb = new PictureBox()
            {
                Image = cImg,
                Height = 75,
                Width = 60,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            return pb;
        }
    }

    class Ace : Card, ICard
    {
        public Ace(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 15;
            TypeOfCard = CardType.Ace;
            SetImg();
        }
    }

    class Six : Card, ICard
    {
        public Six(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 0;
            TypeOfCard = CardType.Six;
            SetImg();
        }
    }

    class Seven : Card, ICard
    {
        public Seven(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 0;
            TypeOfCard = CardType.Seven;
            SetImg();
        }
    }

    class Eight : Card, ICard
    {
        public Eight(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 0;
            TypeOfCard = CardType.Eight;
            SetImg();
        }
    }

    class Nine : Card, ICard
    {
        public Nine(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 0;
            TypeOfCard = CardType.Nine;
            SetImg();
        }
    }

    class Ten : Card, ICard
    {
        public Ten(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 10;
            TypeOfCard = CardType.Ten;
            SetImg();
        }
    }

    class Jack : Card, ICard
    {
        public Jack(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = 20;
            TypeOfCard = CardType.Jack;
            SetImg();
        }
    }

    class Queen : Card, ICard
    {
        public Queen(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = SetSpecValue();
            TypeOfCard = CardType.Queen;
            SetImg();
        }

        int SetSpecValue()
        {
            if (SuitOfCard == CardSuit.Spades) return 50;
            return 10;
        }
    }

    class King : Card, ICard
    {
        public King(int suit)
        {
            SuitOfCard = (CardSuit)suit;
            Value = SetSpecValue();
            TypeOfCard = CardType.King;
            SetImg();
        }

        int SetSpecValue()
        {
            if (SuitOfCard == CardSuit.Hearts) return 30;
            return 10;
        }
    }
}
