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

        List<object> GameProcessLog { get; }

        void AddEventToGameProcessLog(object itm);
    }
}
