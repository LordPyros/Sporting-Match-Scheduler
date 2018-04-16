using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Match_Maker_2
{
    public static class WriteBackData
    {
        public static void writeBackData()
        {
            int counter = 0;
            // write played against files
            foreach (Team newItem in Program.itemList)
            {
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.itemList[counter].TeamName + "PA.csv"))
                {
                    foreach (string value in newItem.PlayedAgainst)
                    {
                        tw.WriteLine(value);
                    }
                }
                counter++;
            }
            counter = 0;
            // write CantPlay times file
            foreach (Team newItem in Program.itemList)
            {
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.itemList[counter].TeamName + "CP.csp"))
                {
                    foreach (string value in newItem.CantPlay)
                    {
                        tw.WriteLine(value);
                    }
                }
                counter++;
            }
            counter = 0;
            // write TimesPlayed file
            foreach (Team newItem in Program.itemList)
            {
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.itemList[counter].TeamName + "TP.csv"))
                {
                    foreach (string value in newItem.TimesPlayed)
                    {
                        tw.WriteLine(value);
                    }
                }
                counter++;
            }
            // write teams file
            using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + "Teams.csv"))
            {
                foreach (Team newItem in Program.itemList)
                {
                    tw.WriteLine(newItem.TeamName + "," + newItem.LSOP + "," + newItem.GamesPlayed + "," + newItem.Byes);

                }
            }
            // write week file
            using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week.csv"))
            {
                tw.WriteLine(Program.week);
            }
            // write matchup file

            if (Program.week != 0)
            {
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.week + ".csv"))
                {
                    foreach (MatchUp value in Program.matchUpListFinal)
                    {
                        tw.WriteLine(value.Time + "," + value.TeamName + "," + value.OpponentTeamName);
                    }
                }
                // write byes file
                using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week" + Program.week + "Byes.csv"))
                {
                    foreach (Team value in Program.byeList)
                    {
                        tw.WriteLine(value.TeamName);
                    }
                }

            }
        }
    }
}
