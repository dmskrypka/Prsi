using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prsi.Objects
{
    public enum PlayerType
    {
        PlayerOne,
        PlayerTwo,
        PC
    }

    class Player
    {
        string name;
        PlayerType pType;
        int points;
        int NumCards;

        public bool PlMove=false;
        public object[] PlayerDeck = new object[36];

        public void SetNew(string name)
        {
            this.name = name;
            this.points = 0;
            this.NumCards = 0;
        }

        public void ChangePoints(int val)
        {
            this.points += val;
        }

        public int ShowPlayerPoints()
        {
            return this.points;
        }

        public int GetCardsValue()
        {
            int res = 0;
            if (this.NumCards > 0)
            {
                Card c;
                for(int i=0;i< this.NumCards; i++)
                {
                    c = (Card)this.PlayerDeck[i];
                    res+=c.GetCardValue();
                }
            }
            return res;
        }

        public Card[] ReturnArray()
        {
            Card[] arr = new Card[this.NumCards];
            for(int i = 0; i < this.NumCards; i++)
                arr[i] = (Card)this.PlayerDeck[i];
            return arr;
        }

        public void TakeCard(object Card)
        {
            this.PlayerDeck[NumCards] = Card;
            this.NumCards++;
        }

        public int NumberOfCards()
        {
            return NumCards;
        }

        public Card GetLastCard()
        {
            return (Card)this.PlayerDeck[this.NumCards-1];
        }

        public void DeleteCard(object card)
        {
            for(int i = 0; i < this.PlayerDeck.Length; i++)
            {
                if (this.PlayerDeck[i] == card)
                {
                    this.PlayerDeck[i] = null;
                    while (this.PlayerDeck[i + 1] != null)
                    {
                        this.PlayerDeck[i] = this.PlayerDeck[i + 1];
                        this.PlayerDeck[i+1] = null;
                        i++;
                    }
                    break;
                }
            }
            this.NumCards--;
        }
    }
}
