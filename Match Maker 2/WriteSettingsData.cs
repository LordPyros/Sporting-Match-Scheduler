using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Match_Maker_2
{
    public static class WriteSettingsData
    {
        public static void writeSettingsData()
        {
            using (TextWriter tw = new StreamWriter(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\Settings.csv"))
            {
                foreach (Settings value in Program.settingsList)
                {
                    tw.WriteLine(value.GameTime + "," + value.NumberOfGameAtThatTime);
                }
            }
            
        }
    }
}
