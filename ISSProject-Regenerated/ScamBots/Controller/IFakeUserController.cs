using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject_Regenerated.ScamBots.Controller
{
    internal interface IFakeUserController
    {
        bool WriteLogToConsole { get; set; }

        void StartBotMechanism();

        void StartAttackWave();

        int GenerateBotAccounts();

        int SendScamMessages();
    }
}
