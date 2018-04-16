using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Maker_2
{
    class MatchUp
    {
        private string time;
        private string teamName;
        private int timesPlayed;
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
        public int TimesPlayed
        {
            get { return timesPlayed; }
            set { timesPlayed = value; }
        }
        public string OpponentTeamName
        {
            get { return opponentTeamName; }
            set { opponentTeamName = value; }
        }
        
        
    }
}
