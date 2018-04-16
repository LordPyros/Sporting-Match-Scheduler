using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Match_Maker_2
{
    public static class LoadTeamData
    {
        public static void loadTeamData()
        {
            string lineOfText;
            Program.itemList.Clear();
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\Teams.csv"))
            {
                //Reads csv file
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\Teams.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);

                while ((lineOfText = file.ReadLine()) != null)
                {
                    // splits the values        
                    Team newItem = new Team();
                    string[] elements = lineOfText.Split(',');
                    //assign values
                    newItem.TeamName = elements[0];
                    newItem.LSOP = Int32.Parse(elements[1]);
                    newItem.GamesPlayed = Int32.Parse(elements[2]);
                    newItem.Byes = Int32.Parse(elements[3]);
                    // add to main list
                    Program.itemList.Add(newItem);
                }
                filestream.Close();
            }
            // read played against files, then add them to itemlist
            int count = 0;
            foreach (Team value in Program.itemList)
            {
                Program.currentTeamName = value.TeamName;
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "PA.csv"))
                {
                    var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "PA.csv", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        Program.itemList[count].PlayedAgainst.Add(lineOfText);

                    }
                    count++;
                    filestream.Close();
                }
            }
            count = 0;
            // read CantPlay files and add to itemlist
            foreach (Team value in Program.itemList)
            {
                Program.currentTeamName = value.TeamName;
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "CP.csp"))
                {
                    var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "CP.csp", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        Program.itemList[count].CantPlay.Add(lineOfText);

                    }
                    count++;
                    filestream.Close();
                }
            }
            count = 0;
            // read TimesPlayed files and add to itemlist
            foreach (Team value in Program.itemList)
            {
                Program.currentTeamName = value.TeamName;
                if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "TP.csv"))
                {
                    var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\" + Program.currentTeamName + "TP.csv", FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        Program.itemList[count].TimesPlayed.Add(lineOfText);

                    }
                    count++;
                    filestream.Close();
                }
            }

        }
    }
}
