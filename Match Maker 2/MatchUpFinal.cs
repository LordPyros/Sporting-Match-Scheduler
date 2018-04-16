using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Maker_2
{
    class MatchUpFinal
    {
        private string time;
        private string teamName;
        private string vs = " VS ";
        private string opponentTeamName;
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }
        public string VS
        {
            get { return vs; }
            set { vs = value; }
        }
        public string OpponentTeamName
        {
            get { return opponentTeamName; }
            set { opponentTeamName = value; }
        }
    }
}
