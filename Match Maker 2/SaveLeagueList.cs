using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Match_Maker_2
{
    public static class SaveLeagueList
    {
        public static void  saveLeagueList()
        {
            using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\Leagues.csv"))
            {
                foreach (string value in Program.leagueNames)
                {
                    tw.WriteLine(value);
                }
            }
        }
    }
}
