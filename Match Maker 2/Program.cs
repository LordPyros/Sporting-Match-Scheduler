using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Match_Maker_2
    
{
    static class Program
    {
        // Main Data List
        public static List<Team> itemList = new List<Team>();
        // List of game times and number of games to be played at thosetimes
        public static List<Settings> settingsList = new List<Settings>();
        // temp list that can be altered during putting teams into timeslots
        public static List<Settings> settingsListTemp = new List<Settings>();
        // total number of teams in competition
        public static int numberOfTeams = 0;
        // total number of available game slots * 2
        public static int numberOfGames = 0;
        // the TeamName of the current team that is being dealt with
        public static string currentTeamName;
        // used when switching timeslots in match make
        public static string secondGameTime;
        // the TeamName of the second team for a match make
        public static string secondTeamName;
        // the TeamName of the first team we're looking to swap with
        public static string thirdTeamName;
        public static string forthTeamName;
        public static string fifthTeamName;
        public static string sixthTeamName;
        // the number of games these teams have played this season
        public static int currentTeamNameGamesPlayed = 0;
        public static int secondTeamNameGamesPlayed = 0;
        // list of teams who had byes this week
        public static List<Team> byeList = new List<Team>();
        // list of teams still available for match making this week
        public static List<Team> matchMakeList = new List<Team>();
        public static List<Team> matchMakeListTemp = new List<Team>();
        // contains teamnames for matchups
        public static List<MatchUp> matchUpList = new List<MatchUp>();
        public static List<MatchUp> matchUpListTemp = new List<MatchUp>();
        public static List<MatchUp> matchUpListFinal = new List<MatchUp>();
        // raises by 1 each time matchMake is run
        public static int week = 0;
        // a counter that controls how many times teams can have played against eachother
        public static int currentPlayedAgainstCounter = 0;
        // becomes 1 when all match making slots are filled
        public static int matchUpsComplete = 0;
        // holds the number of matches created successfully
        public static int matches = 0;
        // contains the current timeslot being dealt with
        public static string currentGameTime;
        // a list of matches that can play at the current timeslot
        public static List<MatchUp> availableCurrentTime = new List<MatchUp>();
        // alternates between earliest and latest matches during creation
        public static int alternate = 0;
        // this is how many more timeslots there are than teams that can fill them 
        public static int numberOfGamesToRemove = 0;
        // path to programs root directory
        public static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\mm2\\";
        // used for file path
        public static string currentSport;
        // List of league names
        public static List<string> leagueNames = new List<string>();
        public static string currentLeague;
        // used in played against statistics table
        public static List<StatisticsPA> matrix = new List<StatisticsPA>();
        // list of match ups that get bound to a datagridview in match making
        public static List<MatchUpFinal> matchUpFinal = new List<MatchUpFinal>();
        // current week for previous match ups
        public static int weekStats = 0;
        // list of teams that must have byes
        public static List<Team> forcedByeList = new List<Team>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            

        }
        
    }
}
