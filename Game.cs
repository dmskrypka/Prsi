using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Resources;
using Prsi.Properties;

namespace Prsi
{
    class Game : GameInfo
    {
        public Deck MixedCards = new Deck();
        public Deck UsedCards = new Deck();
        public GameInfo MainInfo = new GameInfo();
        public Form1 MainForm;
        public Player[] players = new Player[2];

        /// <summary>
        /// Start new game.
        /// After press button "New game" in Form1
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="GD">New cards deck </param>
        /// <param name="MF">Form1</param>
        public void GameDeck(string name, Deck GD, Form1 MF)
        {
            Deck GameD = GD;
            MainForm = MF;
            GameD.MixCards(MixedCards);
            if (players[0] == null)
            {
                players[0] = NewOne("PC");
                players[1] = NewOne(name);
            }

            MainForm.SetPlayersPoints(players[0].ShowPlayerPoints(), players[1].ShowPlayerPoints());
            StartGame(players);
        }

        /// <summary>
        /// Player's move
        /// </summary>
        /// <param name="p">Player which making move: 0 - PC, 1 - main player</param>
        /// <param name="card">Card which player playing</param>
        private void Move(int p, object card)
        {
            players[p].PlMove = true; //maby must be false ??
            UsedCards.AddCard(card);
            MainForm.AddtoDeck(card);
            string name = (p == 0 ? "PC" : MainForm.ReturnName());
            if (MainInfo == null || MainInfo.id == 0)
            {
                MainInfo = Create(name, card);
                MainInfo.root = MainInfo;
            }
            else MainInfo = Insert(MainInfo, name, card);
            MainForm.flpRemove(p, ((Card)card).index);
            players[p].DeleteCard(card);
            MainForm.EndWithBridge(false, players[1].GetCardsValue(),p);
            if (p == 0)
            {
                players[1].PlMove = CheckCard(players[1], 1);
                if (players[1].PlMove) players[0].PlMove = false;
            }
            else
            {
                players[0].PlMove = CheckCard(players[0], 0);
                if (players[0].PlMove) players[1].PlMove = false;
            }
            if (players[p].NumberOfCards() == 0) EndGame();
            bool test = CheckBridge(p);
            if (!test) // some problem with param "test", form choose_suit may be showed after press button no in form end_form
            {
                Card c = (Card)UsedCards.ReturnCard();
                if (c.index[0] == 7)
                {
                    if (p == 1)
                    {
                        Choose_suit CS = new Choose_suit(this, MainForm);
                        CS.ShowDialog();
                    }
                    else
                    {
                        ResourceManager rm = Resources.ResourceManager;
                        Card[] arr = players[0].ReturnArray();
                        Card res = new Card();
                        Card suit_card = new Card();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (res.CardImg == null) res = arr[i];
                            else if (res.GetCardValue() < arr[i].GetCardValue()) res = arr[i];
                        }
                        suit_card.index[0] = 0;

                        if (res.index[1]==0)
                        {
                            suit_card.SetImg("c", rm);
                            suit_card.index[1] = 0;
                        }
                        else if (res.index[1] == 1)
                        {
                            suit_card.SetImg("s", rm);
                            suit_card.index[1] = 1;
                        }
                        else if (res.index[1] == 2)
                        {
                            suit_card.SetImg("d", rm);
                            suit_card.index[1] = 2;
                        }
                        else if (res.index[1] == 3)
                        {
                            suit_card.SetImg("h", rm);
                            suit_card.index[1] = 3;
                        }
                        MainForm.AddtoDeck(suit_card);
                        UsedCards.AddCard(suit_card);
                    }
                }
            }
        }

        public void PlayingProcess(int p, object card)
        {
            NullDeck();
            Move(p, card);
            while (players[0].PlMove)
            {
                Card c = (Card)UsedCards.ReturnCard();
                int index1 = c.index[0];
                int index2 = c.index[1]; 
                Card[] arr = players[0].ReturnArray();
                Card res = new Card();
                if (index1 == 0) // if jack
                {
                    bool test = false;
                    if (index2 == 1)
                    {
                        for(int i = 0; i < arr.Length; i++)
                        {
                            if (arr[i].index[0] == 8 && arr[i].index[1] == 1)
                            {
                                Move(0, arr[i]); //move queen of spiders
                                test = true;
                                break;
                            }
                        }
                    }
                    if(!test)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (arr[i].index[0] == 0)
                            {
                                if (res.CardImg == null) res = arr[i];
                                else if (res.GetCardValue() < arr[i].GetCardValue()) res = arr[i];
                            }
                        }
                        if (res.CardImg != null) Move(0, res); // move card with max value
                        else TakeUntil(c); 
                    }
                }
                else
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i].index[0] == 0 || arr[i].index[0] == index1 || arr[i].index[1] == index2)
                        {
                            if (res.CardImg == null) res = arr[i];
                            else if (res.GetCardValue() < arr[i].GetCardValue()) res = arr[i];
                        }
                    }
                    if (res.CardImg != null) Move(0, res); // move card with max value
                    else TakeUntil(c);
                }
            }
        }

        public void NullDeck()
        {
            if (MixedCards.GetCardNum() == 0) UsedCards.MixCards(MixedCards);
        }

        /// <summary>
        /// Player will use this function if he don't have needed card in player's deck 
        /// </summary>
        /// <param name="used">Last moved card</param>
        public void TakeUntil(Card used)
        {
            NullDeck();
            Card res = new Card();
            int index1 = used.index[0];
            int index2 = used.index[1];
            while (true)
            {
                TakeCard(players[0], MixedCards.ReturnCard(), 0);
                res = players[0].GetLastCard();
                if (res.index[0] == index1 || res.index[1] == index2)
                {
                    Move(0, res);
                    break;
                }
            }
        }

        public string NextSuit(string suit)
        {
            return suit;
        }

        /// <summary>
        /// Create new player
        /// </summary>
        /// <param name="NewName">New player's name</param>
        /// <returns>
        /// Return new object Player
        /// </returns>
        public Player NewOne(string NewName)
        {
            Player Name = new Player();
            Name.SetNew(NewName);
            return Name;
        }

        public void TakeCard(Player players, object card, int id)
        {
            NullDeck();
            players.TakeCard(MixedCards.ReturnCard());
            MainForm.Add(MixedCards.ReturnCard(), id);
            MixedCards.DeleteCard();
        }

        /// <summary>
        /// Players get there cards and making first move.
        /// </summary>
        /// <param name="players">All players</param>
        public void StartGame(Player[] players)
        {
            int NumCards = 10; //players getting 5 cards in one hand
            int choose; // id of player which make first move
            Random r = new Random();
            choose = r.Next(1);

            while (NumCards != 0)
            {
                if (NumCards > 5) TakeCard(players[choose], MixedCards.ReturnCard(), choose);
                else
                {
                    if (choose == 1) TakeCard(players[0], MixedCards.ReturnCard(), 0);
                    else TakeCard(players[1], MixedCards.ReturnCard(), 1);
                }
                NumCards--;
            }
            object PlayCard = players[choose].PlayerDeck[players[choose].NumberOfCards() - 1];
            PlayingProcess(choose, PlayCard);
        }

        /// <summary>
        /// Checking card information. 
        /// As decision will be take some counts of cards or hold move foe next player, or nothing.
        /// </summary>
        /// <param name="name">The player who will be affected by the result.</param>
        /// <param name="id">Player's id</param>
        public bool CheckCard(Player name, int id)
        {
            NullDeck();
            bool res = true;
            Card c = (Card)UsedCards.ReturnCard();
            int count = c.ShowInf()[0];
            int hold = c.ShowInf()[1];
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                    TakeCard(name, UsedCards.ReturnCard(), id);
            }
            if (hold == 1) res = false;
            return res;
        }

        public bool CheckBridge(int p)//Add desigion for PC - DONE!
        {
            bool test = false;
            if (UsedCards.GetCardNum() >= 4)
            {
                int count = 0;
                int type = -1;
                Card c;
                for (int i = UsedCards.GetCardNum() - 1; i >= 0; i--)
                {
                    c = (Card)UsedCards.ReturnCard(i);
                    if (type == -1) type = c.index[0];
                    else if (type != c.index[0]) break;
                    count++;
                    if (count == 4)
                    {
                        MainForm.EndWithBridge(true, players[1].GetCardsValue(),p);
                        test = true;
                    }
                }
            }
            return test;
        }

        public void SetPlayerPoints()
        {
            int value = -125;
            players[0].ChangePoints(players[0].GetCardsValue());
            if (players[0].ShowPlayerPoints() == 125) players[0].ChangePoints(value);

            players[1].ChangePoints(players[1].GetCardsValue());
            if (players[1].ShowPlayerPoints() == 125) players[1].ChangePoints(value);
        }

        public int ShowPoints(int player)
        {
            return players[player].ShowPlayerPoints();
        }

        /// <summary>
        /// Hide Form1 and show End_Form
        /// </summary>
        public void EndGame()
        {
            SetPlayerPoints();
            MainForm.Hide();
            End_Form ef = new End_Form(MainForm.ReturnName(), MainForm, this);
            ef.NewGI(this.MainInfo);
            ef.ShowDialog();
        }
    }
}

