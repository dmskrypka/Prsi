using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prsi.Properties;
using System.Resources;

namespace Prsi
{
    class Deck
    {
        int NumOfCards;
        object[] Cards;

        public Deck()
        {
            this.NumOfCards = 0;
            this.Cards = new object[36];
        }

        /// <summary>
        /// Get card from deck
        /// </summary>
        public object ReturnCard()
        {
            return this.Cards[NumOfCards - 1];
        }

        public object ReturnCard(int num)
        {
            if (num < this.NumOfCards && num >= 0)
                return this.Cards[num];
            else return null;
        }

        public void AddCard(object OneCard)
        {
            if (NumOfCards <= 35)
            {
                this.Cards[NumOfCards] = OneCard;
                this.NumOfCards++;
            }
        }

        public int GetCardNum()
        {
            return this.NumOfCards;
        }

        public void DeleteCard()
        {
            if (NumOfCards > 0)
            {
                this.Cards[NumOfCards - 1] = null;
                this.NumOfCards--;
            }
        }

        /// <summary>
        /// Mixing cards deck
        /// </summary>
        /// <param name="Mix">not mixed deck</param>
        public void MixCards(Deck Mix)
        {
            int max, tmp, i = 0;
            if (this.NumOfCards == 36) max = 35;
            else max = this.NumOfCards - 1;
            while (i < 36)
            {
                Random r = new Random();
                tmp = r.Next(max);
                if (this.Cards[tmp] != null && ((Card)this.Cards[tmp]).index[0]!=0)
                {
                    Mix.AddCard(this.Cards[tmp]);
                    this.Cards[tmp] = this.Cards[max];
                    this.DeleteCard();
                    i++;
                    max--;
                }
                else continue;
            }
        }

        /// <summary>
        /// Creating new deck.
        /// </summary>
        /// <returns>
        /// Not mix deck.
        /// </returns>
        public Deck CardDeck()
        {
            ResourceManager rm = Resources.ResourceManager;
            string[] Card_type = { "Ace", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            string[] Card_suit = { "clubs", "spades", "diamonds", "hearts" };
            string[,] M = new string[Card_suit.GetLength(0), Card_type.GetLength(0)];
            Deck AllCards = new Deck();

            for (int i = 0; i < M.GetLength(0); i++)
            {
                for (int j = 0; j < M.GetLength(1); j++)
                {
                    switch (j)
                    {
                        case 0:
                            Ace _ace = new Ace(i);
                            _ace.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _ace.SetImg(Card_type[j][0] +""+ Card_suit[i][0],rm);
                            _ace.index[0] = 1;
                            _ace.index[1] = i;
                            AllCards.AddCard(_ace);
                            break;
                        case 1:
                            Six _six = new Six(i);
                            _six.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _six.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _six.index[0] = 2;
                            _six.index[1] = i;
                            AllCards.AddCard(_six);
                            break;
                        case 2:
                            Seven _seven = new Seven(i);
                            _seven.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _seven.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _seven.index[0] = 3;
                            _seven.index[1] = i;
                            AllCards.AddCard(_seven);
                            break;
                        case 3:
                            Eight _eight = new Eight(i);
                            _eight.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _eight.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _eight.index[0] = 4;
                            _eight.index[1] = i;
                            AllCards.AddCard(_eight);
                            break;
                        case 4:
                            Nine _nine = new Nine(i);
                            _nine.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _nine.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _nine.index[0] = 5;
                            _nine.index[1] = i;
                            AllCards.AddCard(_nine);
                            break;
                        case 5:
                            Ten _ten = new Ten(i);
                            _ten.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _ten.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _ten.index[0] = 6;
                            _ten.index[1] = i;
                            AllCards.AddCard(_ten);
                            break;
                        case 6:
                            Jack _jack = new Jack(i);
                            _jack.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _jack.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _jack.index[0] = 7;
                            _jack.index[1] = i;
                            AllCards.AddCard(_jack);
                            break;
                        case 7:
                            Queen _queen = new Queen(i);
                            _queen.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _queen.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _queen.index[0] = 8;
                            _queen.index[1] = i;
                            AllCards.AddCard(_queen);
                            break;
                        case 8:
                            King _king = new King(i);
                            _king.SetName(Card_type[j] + " of " + Card_suit[i]);
                            _king.SetImg(Card_type[j][0] + "" + Card_suit[i][0], rm);
                            _king.index[0] = 9;
                            _king.index[1] = i;
                            AllCards.AddCard(_king);
                            break;
                    }
                }
            }
            return AllCards;
        }
    }
}
