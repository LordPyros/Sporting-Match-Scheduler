using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Maker_2
{
    class Settings
    {
        private string gameTime;
        private int numberOfGamesAtThatTime;
        public string GameTime
        {
            get { return gameTime; }
            set { gameTime = value; }
        }
        public int NumberOfGameAtThatTime
        {
            get { return numberOfGamesAtThatTime; }
            set { numberOfGamesAtThatTime = value; }
        }


    }
}
