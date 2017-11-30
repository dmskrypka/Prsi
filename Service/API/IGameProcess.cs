using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prsi.Objects;

namespace Prsi.Service.API
{
    interface IGameProcess
    {
        // TODO: set new class of card
        object CardMove(PlayerType pType, object card);

        List<IGameEventInfo> GameProcessLog { get; }

        void AddEventToGameProcessLog();
    }

    interface IGameEventInfo
    {
        string PlayerName { get; }
        string cardName { get; }
        DateTime TimeOfEvent { get; }
        int GamePointsState { get; }
    }
}
