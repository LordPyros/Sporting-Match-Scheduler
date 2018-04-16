using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Match_Maker_2
{
    public static class ReadGameTimesFile
    {
        public static void readGameTimesFile()
        {
            Program.settingsList.Clear();
            // reads settings file into variables
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\Settings.csv"))
            {
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\Settings.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                string lineOfText;
                while ((lineOfText = file.ReadLine()) != null)
                {
                    string[] elements = lineOfText.Split(',');
                    Settings newItem = new Settings();
                    newItem.GameTime = elements[0];
                    newItem.NumberOfGameAtThatTime = Int32.Parse(elements[1]);
                    Program.settingsList.Add(newItem);
                }
                filestream.Close();
            }
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week.csv"))
            {
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\week.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                string lineOfText;
                while ((lineOfText = file.ReadLine()) != null)
                {
                    Program.week = Int32.Parse(lineOfText);
                }
                filestream.Close();

            }
        }
    }
}
