using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prsi
{
    /// <summary>
    /// Class for writing players moves in linked list.
    /// </summary>
    public class GameInfo
    {
        public int id;
        public string PlayerName;
        public string info;
        public object card;
        public GameInfo root;
        public GameInfo next;
        public GameInfo prev;
        
        public int IDnum = 1;

        /// <summary>
        /// Creating new element.
        /// </summary>
        /// <param name="name">Name of player which made move</param>
        /// <param name="card">Played card</param>
        /// <returns>
        /// New GameInfo element.
        /// </returns>
        public GameInfo Create(string name, object card)
        {
            GameInfo GI = new GameInfo();
            GI.PlayerName = name;
            GI.card = card;
            GI.id = IDnum;
            GI.next = null;
            GI.prev = null;
            IDnum += 1;
            return GI;
        }

        public GameInfo Insert(GameInfo old, string name, object card)
        {
            GameInfo GI = Create(name, card);
            old.next = GI;
            GI.prev = old;
            GI.root = old.root;
            return GI;
        }

        public GameInfo Delete(GameInfo GI, GameInfo move)
        {
            if (GI == null) return null;
            if (move == null) return GI;
            if (GI == move) GI = move.next;
            if (move.prev != null) move.prev.next = move.next;
            if (move.next != null) move.next.prev = move.prev;
            return GI;
        }
    }
}
