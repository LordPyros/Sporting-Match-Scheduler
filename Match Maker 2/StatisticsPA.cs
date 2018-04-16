using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Maker_2
{
    class StatisticsPA
    {
        private List<int> statPlayedAgainst = new List<int>();
        private string statTeamName;
        public List<int> StatPlayedAgainst
        {
            get { return statPlayedAgainst; }
            set { statPlayedAgainst = value; }
        }
        public string StatTeamName
        {
            get { return statTeamName; }
            set { statTeamName = value; }
        }
    }
}
