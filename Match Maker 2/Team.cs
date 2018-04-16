using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Maker_2
{
    class Team
    {
        private string teamName;
        private List<string> cantPlay = new List<string>();
        private int lSOP;
        private List<string> timesPlayed = new List<string>();
        private int playedThisWeek;
        private int gamesPlayed;
        private int byes;
        private List<string> playedAgainst = new List<string>();
        private int playedAt;
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }
        public List<string> CantPlay
        {
            get { return cantPlay; }
            set { cantPlay = value; }
        }
        public int LSOP
        {
            get { return lSOP; }
            set { lSOP = value; }
        }
        public List<string> TimesPlayed
        {
            get { return timesPlayed; }
            set { timesPlayed = value; }
        }
        public int PlayedThisWeek
        {
            get { return playedThisWeek; }
            set { playedThisWeek = value; }
        }
        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set { gamesPlayed = value; }
        }
        public int Byes
        {
            get { return byes; }
            set { byes = value; }
        }
        public List<string> PlayedAgainst
        {
            get { return playedAgainst; }
            set { playedAgainst = value; }
        }
        public int PlayedAt
        {
            get { return playedAt; }
            set { playedAt = value; }
        }
    }
}
