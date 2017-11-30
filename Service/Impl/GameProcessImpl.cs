using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prsi.Service.API;


namespace Prsi.Service.Impl
{
    class GameProcessImpl : IGameProcess
    {
        IGameConfig gameConfig;
        public List<IGameEventInfo> GameProcessLog { get; set; }

        public GameProcessImpl()
        {
            gameConfig = new GameConfigImpl();
        }

        public void AddEventToGameProcessLog()
        {
            this.GameProcessLog.Add(new GameEventInfoImpl(this));
        }
    }

    class GameEventInfoImpl : IGameEventInfo
    {
        public string PlayerName { get; set; }
        public string cardName { get; set; }
        public DateTime TimeOfEvent { get; set; }
        public int GamePointsState { get; set; }

        public GameEventInfoImpl(IGameProcess currentGameProcess)
        {
            

        }
    }
}
