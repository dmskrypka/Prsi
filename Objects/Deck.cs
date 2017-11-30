using System;
using System.Collections.Generic;

namespace Prsi.Objects
{
    interface IDeck
    {
        List<ICard> DeckWithCards { get; }
        List<ICard> GetNewGamingDeck();
        void DeleteCardFromDeck(ICard card);

    }

    class DeckImpl : IDeck
    {
        public List<ICard> DeckWithCards { get; set; }
        private Random random = new Random();

        public DeckImpl() { }

        public List<ICard> GetNewGamingDeck()
        {
              DeckGenerator();
              return MixCards();
        }
         
        private void DeckGenerator()
        {
            DeckWithCards = new List<ICard>();
            CardFactory cFactory = new CardFactory();
            DeckImpl AllCards = new DeckImpl();

            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    DeckWithCards.Add(cFactory.GetCard(i, j));
                }
            }
        }

        private List<ICard> MixCards()
        {
            int tmp, CountOfCards = DeckWithCards.Capacity;
            while(CountOfCards>1)
            {
                CountOfCards--;
                tmp = random.Next(CountOfCards+1);
                CardsSwap(DeckWithCards[CountOfCards],DeckWithCards[tmp]);
            }
            return DeckWithCards;
        }

        private void CardsSwap(ICard firstCard, ICard secondCard)
        {
            ICard tmp = firstCard;
            firstCard = secondCard;
            secondCard = tmp;
        }

        public void DeleteCardFromDeck(ICard card)
        {
            this.DeckWithCards.Remove(card);
        }
    }
}
