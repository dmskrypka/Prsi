using System.Collections.Generic;

namespace Prsi.Objects
{
    interface IPlayer
    {
        string Name { get; }
        PlayerType Type { get; }
        int GamePoints { get; set; }
        List<ICard> PlayersDeck { get; }

        int GetPlayerCardsValue();
        void AddCardToPlayersDeck(ICard card);
        void DeleteCardFromPlayersDeck(ICard card);
    }

    public enum PlayerType
    {
        Player,
        PC
    }

    class PlayerImpl : IPlayer
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public int GamePoints { get; set; }
        public List<ICard> PlayersDeck { get; set; }

        public PlayerImpl(string _name)
        {
            this.Name = _name;
            this.Type = PlayerType.Player;
            this.GamePoints = 0;
            this.PlayersDeck = new List<ICard>();
        }

        public PlayerImpl()
        {
            this.Name = "PC";
            this.Type = PlayerType.PC;
            this.GamePoints = 0;
            this.PlayersDeck = new List<ICard>();
        }

        public int GetPlayerCardsValue()
        {
            int res = 0;
            foreach(ICard card in PlayersDeck)
                res += card.Value;
            return res;
        }

        public void AddCardToPlayersDeck(ICard card)
        {
            this.PlayersDeck.Add(card);
        }

        public void DeleteCardFromPlayersDeck(ICard card)
        {
            this.PlayersDeck.Remove(card);
        }
    }
}
