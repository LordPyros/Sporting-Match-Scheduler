using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Match_Maker_2.Properties;
namespace Match_Maker_2
{
    public partial class MainMatchMake : Form
    {
        public MainMatchMake()
        {
            InitializeComponent();

            // need to allow 2x click on all lists
        }
        // edit team button
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            EditTeam editTeam = new EditTeam();
            editTeam.ShowDialog();
        }
        // Match Making Button
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // need to address issue when not all matches are made successfully
            MakeBackUp();
            ReadGameTimesFile.readGameTimesFile();
            LoadTeamData.loadTeamData();
            CheckForForcedByes();
            //var rnd = new Random();
            //Program.itemList = Program.itemList.OrderBy(x => rnd.Next()).ToList();
            GetNumberOfTeams();
            GetNumberOfGames();
            CheckNumberOfTeamsAndGames();
            CheckToIncreaseCurrentPlayedAgainstCounter();
            //CheckForForcedByes();
            CheckForByes();
            MatchMake();
            SwapTeamsWithBye();
            SwapTeamsBasic();
            SwapTeamsComplex();
            SwapTeamsComplexReverse();
            RandomRetryMakeMatch();
            UpdatePlayedAgainst();
            PlaceTeams();
            // if not randomise list and retry
            // if still not set complete to 0 and run RandomRetryMakeMatch
            foreach (Team value in Program.forcedByeList)
            {
                Program.byeList.Add(value);
            }
            OutputMatches();
            SetTimesPlayed();

            WriteBackData.writeBackData();
            ShowMatchesForm showMatchesForm = new ShowMatchesForm();
            showMatchesForm.ShowDialog();
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
            {
                File.Delete(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv");
            }
        }

        void MakeBackUp()
        {
            // creates a backup of all csv files in current league
            if (!Directory.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup"))
            {
                Directory.CreateDirectory(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup");
            }
            Array.ForEach(Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\"), File.Delete);
            string[] txtFiles = Directory.GetFiles(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\", "*.csv");
            foreach (var item in txtFiles)
            {
                File.Copy(item, Path.Combine(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\", Path.GetFileName(item)));
            }

        }
        void GetNumberOfTeams()
        {
            Program.numberOfTeams = Program.itemList.Count - Program.forcedByeList.Count;
        }
        void GetNumberOfGames()
        {
            Program.numberOfGames = 0;
            foreach (Settings value in Program.settingsList)
            {
                Program.numberOfGames = (Program.numberOfGames + value.NumberOfGameAtThatTime);
            }
            Program.numberOfGames = Program.numberOfGames * 2;
        }
        void CheckNumberOfTeamsAndGames()
        {
            // checks to ensure there are enough team to fill all timeslots, if not it reduces the number of matches to be created
            Program.numberOfGamesToRemove = 0;
            if (Program.numberOfGames > Program.numberOfTeams)
            {
                Program.numberOfGamesToRemove = ((Program.numberOfGames / 2) - (Program.numberOfTeams / 2));
                Program.numberOfGames = ((Program.numberOfGames / 2) - Program.numberOfGamesToRemove);
                Program.numberOfGames = (Program.numberOfGames * 2);
            }
        }
        
        void CheckForByes()
        {
            Program.byeList.Clear();
            List<Team> sortedListGamesPlayed = Program.itemList.OrderByDescending(o => o.GamesPlayed).ToList();
            int teamsByed = 0;
            int loopCounter = 0;
            int byeCount = (Program.numberOfTeams - Program.numberOfGames);
            if (byeCount > 0)
            {
                while (byeCount != teamsByed)
                {
                    // select a Team that hasn't been bye'd this week
                    Program.currentTeamName = sortedListGamesPlayed[loopCounter].TeamName;
                    loopCounter++;
                    // Add 1 to byes and playedThisWeek
                    foreach (Team newTeam in Program.itemList)
                    {
                        if (newTeam.TeamName == Program.currentTeamName)
                        {
                            bool alreadyByed = false;
                            // need to check that team is not in forcedByeList
                            foreach (Team t in Program.forcedByeList)
                            {
                                if (t.TeamName == Program.currentTeamName)
                                {
                                    alreadyByed = true;
                                }
                            }
                            if (!alreadyByed)
                            {
                                newTeam.Byes++;
                                newTeam.PlayedThisWeek++;
                                teamsByed++;
                                Program.byeList.Add(newTeam);
                            }
                        }
                    }
                }
            }
        }

        void CheckForForcedByes()
        {
            Program.forcedByeList.Clear();
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv"))
            {
                var filestream = new FileStream(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\bye.csv", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite);
                var file = new StreamReader(filestream, Encoding.UTF8, true, 128);
                string lineOfText;
                while ((lineOfText = file.ReadLine()) != null)
                {
                    foreach (Team value in Program.itemList)
                    {
                        if (lineOfText == value.TeamName)
                        {
                            Program.forcedByeList.Add(value);
                        }
                    }
                }
                filestream.Close();
            }
            foreach (Team value in Program.forcedByeList)
            {
                foreach (Team team in Program.itemList)
                {
                    if (value.TeamName == team.TeamName)
                    {
                        team.PlayedThisWeek++;
                        team.Byes++;
                    }
                }
            }
        }

        void MatchMake()
        {
            Program.matchUpsComplete = 0;
            Program.week++;
            CheckToIncreaseCurrentPlayedAgainstCounter();
            Console.WriteLine();
            Console.WriteLine("WEEK " + Program.week);
            Program.matchUpList.Clear();
            Program.matchMakeList.Clear();
            int counter = 0;
            // will loop once for each match to be made
            while ((Program.numberOfGames / 2) != counter)
            {
                int count = 100;
                int p = 1;
                while (p < count)
                {
                    int timesPlayedAgainstCounter = 0;
                    count = 0;
                    Program.matchMakeList.Clear();
                    // Make a list of available teams
                    foreach (Team value in Program.itemList)
                    {
                        if (value.PlayedThisWeek == 0)
                        {
                            Program.matchMakeList.Add(value);
                            count++;
                        }
                    }
                    // select 1st team in new list
                    Program.currentTeamName = Program.matchMakeList[0].TeamName;
                    // check each team until one is found that hasn't played current team already this season
                    Program.secondTeamName = Program.matchMakeList[p].TeamName;
                    p++;
                    foreach (Team value in Program.itemList)
                    {
                        if (Program.secondTeamName == value.TeamName)
                        {
                            foreach (string values in value.PlayedAgainst)
                            {
                                if (values == Program.currentTeamName)
                                {
                                    timesPlayedAgainstCounter++;
                                }
                            }
                        }
                    }
                    if (timesPlayedAgainstCounter <= Program.currentPlayedAgainstCounter)
                    {
                        MatchUp newMatchUp = new MatchUp();
                        foreach (Team value in Program.itemList)
                        {
                            if (value.TeamName == Program.currentTeamName)
                            {
                                value.PlayedThisWeek++;
                                newMatchUp.TeamName = Program.currentTeamName;
                            }
                            if (value.TeamName == Program.secondTeamName)
                            {
                                value.PlayedThisWeek++;
                                newMatchUp.OpponentTeamName = Program.secondTeamName;
                            }
                        }
                        Program.matchUpList.Add(newMatchUp);
                        p = count;
                        Console.WriteLine(Program.currentTeamName + " vs " + Program.secondTeamName);
                    }
                }
                counter++;
            }

        }
        void RandomRetryMakeMatch()
        {
            CheckMatchesMade();
            if (Program.matchUpsComplete == 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    CheckMatchesMade();
                    if (Program.matchUpsComplete == 0)
                    {
                        Program.week--;
                        ReadGameTimesFile.readGameTimesFile();
                        LoadTeamData.loadTeamData();
                        GetNumberOfTeams();
                        GetNumberOfGames();
                        CheckNumberOfTeamsAndGames();
                        var rnd = new Random();
                        Program.itemList = Program.itemList.OrderBy(x => rnd.Next()).ToList();
                        CheckForForcedByes();
                        CheckForByes();
                        MatchMake();
                        SwapTeamsWithBye();
                        SwapTeamsBasic();
                        SwapTeamsComplex();
                        SwapTeamsComplexReverse();
                        // test
                        CheckMatchesMade();
                        if (Program.matchUpsComplete != 0)
                        {
                            Console.WriteLine("Random Redo Successful on attempt no " + i);
                        }
                    }
                }
            }
        }
        void UpdatePlayedAgainst()
        {
            foreach (MatchUp value in Program.matchUpList)
            {
                Program.currentTeamName = value.TeamName;
                Program.secondTeamName = value.OpponentTeamName;
                foreach (Team teamValue in Program.itemList)
                {
                    if (teamValue.TeamName == Program.currentTeamName)
                    {
                        teamValue.GamesPlayed++;
                        teamValue.PlayedAgainst.Add(Program.secondTeamName);
                    }
                    if (teamValue.TeamName == Program.secondTeamName)
                    {
                        teamValue.GamesPlayed++;
                        teamValue.PlayedAgainst.Add(Program.currentTeamName);
                    }
                }
            }
        }
        void SelectTeamsForMatchMaking()
        {
            Program.matchMakeList.Clear();
            // Make a list of available teams
            foreach (Team value in Program.itemList)
            {
                if (value.PlayedThisWeek == 0)
                {
                    Program.matchMakeList.Add(value);
                }
            }
            Program.currentTeamName = Program.matchMakeList[0].TeamName;
            Program.secondTeamName = Program.matchMakeList[1].TeamName;
        }
        void SwapTeamsBasic()
        {
            CheckMatchesMade();
            // Did not swap the 2 teams without matchups yet
            if (Program.matchUpsComplete == 0)
            {
                for (int i = 0; i < ((Program.numberOfGames / 2) - Program.matches); i++)
                {
                    Console.WriteLine("Swap Teams Basic Called");
                    SelectTeamsForMatchMaking();

                    int paCounter = 0;
                    int count = 0;
                    int complete = 0;
                    foreach (MatchUp value in Program.matchUpList)
                    {
                        if (complete == 0)
                        {
                            // get a teamname, check if they have played current team
                            Program.thirdTeamName = Program.matchUpList[count].TeamName;
                            foreach (Team aTeam in Program.itemList)
                            {
                                if (Program.thirdTeamName == aTeam.TeamName)
                                {
                                    foreach (string pa in aTeam.PlayedAgainst)
                                    {
                                        if (pa == Program.currentTeamName)
                                        {
                                            paCounter++;
                                        }
                                    }
                                }
                            }
                            // if team suitable, check second team with opponent
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                paCounter = 0;
                                Program.forthTeamName = Program.matchUpList[count].OpponentTeamName;
                                foreach (Team aTeam in Program.itemList)
                                {
                                    if (Program.forthTeamName == aTeam.TeamName)
                                    {
                                        foreach (string pa in aTeam.PlayedAgainst)
                                        {
                                            if (pa == Program.secondTeamName)
                                            {
                                                paCounter++;
                                            }
                                        }
                                    }
                                }

                            }
                            List<MatchUp> matchUpListTemp = new List<MatchUp>();
                            // if good, make match
                            // update matchuplist
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                foreach (MatchUp item in Program.matchUpList)
                                {
                                    if (item.TeamName != Program.matchUpList[count].TeamName)
                                    {
                                        matchUpListTemp.Add(item);
                                    }
                                }
                                Program.matchUpList = matchUpListTemp;
                                MatchUp secondNewMatchUp = new MatchUp();
                                MatchUp newMatchUp = new MatchUp();
                                newMatchUp.TeamName = Program.thirdTeamName;
                                newMatchUp.OpponentTeamName = Program.currentTeamName;
                                Program.matchUpList.Add(newMatchUp);
                                secondNewMatchUp.TeamName = Program.forthTeamName;
                                secondNewMatchUp.OpponentTeamName = Program.secondTeamName;
                                Program.matchUpList.Add(secondNewMatchUp);
                                // update played this week
                                foreach (Team item in Program.itemList)
                                {
                                    if (item.TeamName == Program.currentTeamName)
                                    {
                                        item.PlayedThisWeek++;
                                    }
                                    if (item.TeamName == Program.secondTeamName)
                                    {
                                        item.PlayedThisWeek++;
                                    }
                                }
                                Console.WriteLine(Program.currentTeamName + " vs " + Program.thirdTeamName + " Swapped straight");
                                Console.WriteLine(Program.secondTeamName + " vs " + Program.forthTeamName + " Swapped straight");
                                complete++;
                            }
                            // if not try opposite teams
                        }
                        if (complete == 0)
                        {
                            paCounter = 0;
                            // get a teamname, check if they have played current team
                            Program.forthTeamName = Program.matchUpList[count].TeamName;
                            foreach (Team aTeam in Program.itemList)
                            {
                                if (Program.forthTeamName == aTeam.TeamName)
                                {
                                    foreach (string pa in aTeam.PlayedAgainst)
                                    {
                                        if (pa == Program.currentTeamName)
                                        {
                                            paCounter++;
                                        }
                                    }
                                }
                            }
                            // if team suitable, check second team with opponent
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                paCounter = 0;
                                Program.thirdTeamName = Program.matchUpList[count].OpponentTeamName;
                                foreach (Team aTeam in Program.itemList)
                                {
                                    if (Program.thirdTeamName == aTeam.TeamName)
                                    {
                                        foreach (string pa in aTeam.PlayedAgainst)
                                        {
                                            if (pa == Program.secondTeamName)
                                            {
                                                paCounter++;
                                            }
                                        }
                                    }
                                }

                            }
                            List<MatchUp> matchUpListTemp = new List<MatchUp>();
                            // if good, make match
                            // update matchuplist
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                foreach (MatchUp item in Program.matchUpList)
                                {
                                    if (item.TeamName != Program.matchUpList[count].TeamName)
                                    {
                                        matchUpListTemp.Add(item);
                                    }
                                }
                                Program.matchUpList = matchUpListTemp;
                                MatchUp secondNewMatchUp = new MatchUp();
                                MatchUp newMatchUp = new MatchUp();
                                newMatchUp.TeamName = Program.forthTeamName;
                                newMatchUp.OpponentTeamName = Program.currentTeamName;
                                Program.matchUpList.Add(newMatchUp);
                                secondNewMatchUp.TeamName = Program.secondTeamName;
                                secondNewMatchUp.OpponentTeamName = Program.thirdTeamName;
                                Program.matchUpList.Add(secondNewMatchUp);
                                // update played this week
                                foreach (Team item in Program.itemList)
                                {
                                    if (item.TeamName == Program.currentTeamName)
                                    {
                                        item.PlayedThisWeek++;
                                    }
                                    if (item.TeamName == Program.secondTeamName)
                                    {
                                        item.PlayedThisWeek++;
                                    }
                                }
                                Console.WriteLine(Program.currentTeamName + " vs " + Program.forthTeamName + " Swapped crossover");
                                Console.WriteLine(Program.secondTeamName + " vs " + Program.thirdTeamName + " Swapped crossover");
                                complete++;
                            }
                        }
                        count++;
                    }
                }
            }

        }
        void SwapTeamsComplex()
        {
            CheckMatchesMade();
            if (Program.matchUpsComplete == 0)
            {

                for (int i = 0; i < ((Program.numberOfGames / 2) - Program.matches); i++)
                {
                    Console.WriteLine("Swap Teams Complex Called");
                    SelectTeamsForMatchMaking();
                    int paCounter = 0;
                    int count = 0;
                    int complete = 0;
                    foreach (MatchUp value in Program.matchUpList)
                    {
                        if (complete == 0)
                        {
                            // get a teamname, check if they have played current team
                            Program.thirdTeamName = Program.matchUpList[count].TeamName;
                            // save opponent teamname
                            Program.fifthTeamName = Program.matchUpList[count].OpponentTeamName;
                            paCounter = 0;
                            foreach (Team aTeam in Program.itemList)
                            {
                                if (Program.thirdTeamName == aTeam.TeamName)
                                {
                                    foreach (string pa in aTeam.PlayedAgainst)
                                    {
                                        if (pa == Program.currentTeamName)
                                        {
                                            paCounter++;
                                        }
                                    }
                                }
                            }
                            // if team suitable, check second team with every possible opponent
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                foreach (MatchUp newMatchUp in Program.matchUpList) // 6 times currently
                                {
                                    int d = 0;
                                    paCounter = 0;
                                    // select a team to try
                                    Program.forthTeamName = Program.matchUpList[d].OpponentTeamName;
                                    // select their opponent
                                    Program.sixthTeamName = Program.matchUpList[d].TeamName;
                                    // check that the 2 target teams are not already in a match up
                                    if (Program.matchUpList[d].TeamName != Program.thirdTeamName)
                                    {
                                        foreach (Team aTeam in Program.itemList)
                                        {
                                            if (Program.forthTeamName == aTeam.TeamName && complete == 0)
                                            {
                                                foreach (string pa in aTeam.PlayedAgainst)
                                                {
                                                    if (pa == Program.secondTeamName)
                                                    {
                                                        paCounter++;
                                                    }
                                                }
                                                // if second team has found a suitable match, try match the last 2 teams
                                                if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0 && Program.matchUpList[d].TeamName != Program.thirdTeamName)
                                                {

                                                    foreach (Team lastTeam in Program.itemList)
                                                    {
                                                        paCounter = 0;
                                                        if (Program.fifthTeamName == lastTeam.TeamName && complete == 0)
                                                        {
                                                            foreach (string pa in lastTeam.PlayedAgainst)
                                                            {
                                                                if (pa == Program.sixthTeamName)
                                                                {
                                                                    paCounter++;
                                                                }
                                                            }
                                                            // if last teams are suitable, make matches
                                                            if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0)
                                                            {
                                                                List<MatchUp> matchUpListTemp = new List<MatchUp>();
                                                                foreach (MatchUp item in Program.matchUpList)
                                                                {
                                                                    if (item.TeamName != Program.thirdTeamName && item.TeamName != Program.forthTeamName && item.TeamName != Program.fifthTeamName && item.TeamName != Program.sixthTeamName)
                                                                    {
                                                                        matchUpListTemp.Add(item);
                                                                    }
                                                                }
                                                                Program.matchUpList = matchUpListTemp;
                                                                MatchUp newMatchUp2 = new MatchUp();
                                                                MatchUp newMatchUp1 = new MatchUp();
                                                                newMatchUp1.TeamName = Program.thirdTeamName;
                                                                newMatchUp1.OpponentTeamName = Program.currentTeamName;
                                                                Program.matchUpList.Add(newMatchUp1);
                                                                newMatchUp2.TeamName = Program.forthTeamName;
                                                                newMatchUp2.OpponentTeamName = Program.secondTeamName;
                                                                Program.matchUpList.Add(newMatchUp2);
                                                                MatchUp newMatchUp3 = new MatchUp();
                                                                newMatchUp3.TeamName = Program.fifthTeamName;
                                                                newMatchUp3.OpponentTeamName = Program.sixthTeamName;
                                                                Program.matchUpList.Add(newMatchUp3);
                                                                // update played this week
                                                                foreach (Team item in Program.itemList)
                                                                {
                                                                    if (item.TeamName == Program.currentTeamName)
                                                                    {
                                                                        item.PlayedThisWeek++;
                                                                    }
                                                                    if (item.TeamName == Program.secondTeamName)
                                                                    {
                                                                        item.PlayedThisWeek++;
                                                                    }
                                                                }
                                                                Console.WriteLine(Program.currentTeamName + " vs " + Program.thirdTeamName + " Swapped complex");
                                                                Console.WriteLine(Program.secondTeamName + " vs " + Program.forthTeamName + " Swapped complex");
                                                                Console.WriteLine(Program.fifthTeamName + " vs " + Program.sixthTeamName + " Swapped complex");
                                                                complete++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (complete == 0)
                                    {
                                        paCounter = 0;
                                        // select a team to try
                                        Program.forthTeamName = Program.matchUpList[d].TeamName;
                                        // select their opponent
                                        Program.sixthTeamName = Program.matchUpList[d].OpponentTeamName;
                                        // check that the 2 target teams are not already in a match up
                                        if (Program.matchUpList[d].TeamName != Program.thirdTeamName)
                                        {
                                            foreach (Team aTeam in Program.itemList)
                                            {
                                                if (Program.forthTeamName == aTeam.TeamName && complete == 0)
                                                {
                                                    foreach (string pa in aTeam.PlayedAgainst)
                                                    {
                                                        if (pa == Program.secondTeamName)
                                                        {
                                                            paCounter++;
                                                        }
                                                    }

                                                    // if second team has found a suitable match, try match the last 2 teams
                                                    if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0 && Program.matchUpList[d].TeamName != Program.thirdTeamName)
                                                    {
                                                        paCounter = 0;
                                                        foreach (Team lastTeam in Program.itemList)
                                                        {
                                                            if (Program.fifthTeamName == lastTeam.TeamName && complete == 0)
                                                            {
                                                                foreach (string pa in lastTeam.PlayedAgainst)
                                                                {
                                                                    if (pa == Program.sixthTeamName)
                                                                    {
                                                                        paCounter++;
                                                                    }
                                                                }

                                                                // if last teams are suitable, make matches
                                                                if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0)
                                                                {
                                                                    List<MatchUp> matchUpListTemp = new List<MatchUp>();
                                                                    foreach (MatchUp item in Program.matchUpList)
                                                                    {
                                                                        if (item.TeamName != Program.thirdTeamName && item.TeamName != Program.forthTeamName && item.TeamName != Program.fifthTeamName && item.TeamName != Program.sixthTeamName)
                                                                        {
                                                                            matchUpListTemp.Add(item);
                                                                        }
                                                                    }
                                                                    Program.matchUpList = matchUpListTemp;
                                                                    MatchUp newMatchUp2 = new MatchUp();
                                                                    MatchUp newMatchUp1 = new MatchUp();
                                                                    newMatchUp1.TeamName = Program.thirdTeamName;
                                                                    newMatchUp1.OpponentTeamName = Program.currentTeamName;
                                                                    Program.matchUpList.Add(newMatchUp1);
                                                                    newMatchUp2.TeamName = Program.forthTeamName;
                                                                    newMatchUp2.OpponentTeamName = Program.secondTeamName;
                                                                    Program.matchUpList.Add(newMatchUp2);
                                                                    MatchUp newMatchUp3 = new MatchUp();
                                                                    newMatchUp3.TeamName = Program.fifthTeamName;
                                                                    newMatchUp3.OpponentTeamName = Program.sixthTeamName;
                                                                    Program.matchUpList.Add(newMatchUp3);
                                                                    // update played this week
                                                                    foreach (Team item in Program.itemList)
                                                                    {
                                                                        if (item.TeamName == Program.currentTeamName)
                                                                        {
                                                                            item.PlayedThisWeek++;
                                                                        }
                                                                        if (item.TeamName == Program.secondTeamName)
                                                                        {
                                                                            item.PlayedThisWeek++;
                                                                        }
                                                                    }
                                                                    Console.WriteLine(Program.currentTeamName + " vs " + Program.thirdTeamName + " Swapped complex cross");
                                                                    Console.WriteLine(Program.secondTeamName + " vs " + Program.forthTeamName + " Swapped complex cross");
                                                                    Console.WriteLine(Program.fifthTeamName + " vs " + Program.sixthTeamName + " Swapped complex cross");
                                                                    complete++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    d++;
                                }
                            }
                        }
                        count++;
                    }
                }
            }
            // find which teams still need to be matched up
            // check if each team has played current team
            // once suitable team found, check if each team has played second team
            // if curent and second teams have found suitable opponents, check the if the remaining 2 teams can be matched up
        }
        void SwapTeamsComplexReverse()
        {
            CheckMatchesMade();
            if (Program.matchUpsComplete == 0)
            {

                for (int i = 0; i < ((Program.numberOfGames / 2) - Program.matches); i++)
                {
                    Console.WriteLine("Swap Teams Complex Reverse Called");
                    SelectTeamsForMatchMaking();
                    int paCounter = 0;
                    int count = 0;
                    int complete = 0;
                    foreach (MatchUp value in Program.matchUpList)  // increase "count" by 1 each pass
                    {
                        if (complete == 0)
                        {
                            // get a teamname, check if they have played current team
                            Program.thirdTeamName = Program.matchUpList[count].OpponentTeamName;
                            // save opponent teamname
                            Program.fifthTeamName = Program.matchUpList[count].TeamName;
                            paCounter = 0;
                            foreach (Team aTeam in Program.itemList)
                            {
                                if (Program.thirdTeamName == aTeam.TeamName)
                                {
                                    foreach (string pa in aTeam.PlayedAgainst)
                                    {
                                        if (pa == Program.currentTeamName)
                                        {
                                            paCounter++;
                                        }
                                    }
                                }
                            }
                            // if team suitable, check second team with every possible opponent
                            if (paCounter <= Program.currentPlayedAgainstCounter)
                            {
                                int d = 0;
                                foreach (MatchUp newMatchUp in Program.matchUpList) // 6 times currently, increases "d" by 1 each pass
                                {

                                    paCounter = 0;
                                    // select a team to try
                                    Program.forthTeamName = Program.matchUpList[d].TeamName;
                                    // select their opponent
                                    Program.sixthTeamName = Program.matchUpList[d].OpponentTeamName;
                                    // check that the 2 target teams are not already in a match up
                                    if (Program.matchUpList[d].OpponentTeamName != Program.thirdTeamName)
                                    {
                                        foreach (Team aTeam in Program.itemList)
                                        {
                                            if (Program.forthTeamName == aTeam.TeamName && complete == 0)
                                            {
                                                foreach (string pa in aTeam.PlayedAgainst)
                                                {
                                                    if (pa == Program.secondTeamName)
                                                    {
                                                        paCounter++;
                                                    }
                                                }

                                                // if second team has found a suitable match, try match the last 2 teams
                                                if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0 && Program.matchUpList[d].OpponentTeamName != Program.thirdTeamName)
                                                {
                                                    foreach (Team lastTeam in Program.itemList)
                                                    {
                                                        paCounter = 0;
                                                        if (Program.fifthTeamName == aTeam.TeamName && complete == 0)
                                                        {
                                                            foreach (string pa in aTeam.PlayedAgainst)
                                                            {
                                                                if (pa == Program.sixthTeamName)
                                                                {
                                                                    paCounter++;
                                                                }
                                                            }

                                                            // if last teams are suitable, make matches
                                                            if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0)
                                                            {
                                                                List<MatchUp> matchUpListTemp = new List<MatchUp>();
                                                                foreach (MatchUp item in Program.matchUpList)
                                                                {
                                                                    if (item.TeamName != Program.thirdTeamName && item.TeamName != Program.forthTeamName && item.TeamName != Program.fifthTeamName && item.TeamName != Program.sixthTeamName)
                                                                    {
                                                                        matchUpListTemp.Add(item);
                                                                    }
                                                                }
                                                                Program.matchUpList = matchUpListTemp;
                                                                MatchUp newMatchUp2 = new MatchUp();
                                                                MatchUp newMatchUp1 = new MatchUp();
                                                                newMatchUp1.TeamName = Program.thirdTeamName;
                                                                newMatchUp1.OpponentTeamName = Program.currentTeamName;
                                                                Program.matchUpList.Add(newMatchUp1);
                                                                newMatchUp2.TeamName = Program.forthTeamName;
                                                                newMatchUp2.OpponentTeamName = Program.secondTeamName;
                                                                Program.matchUpList.Add(newMatchUp2);
                                                                MatchUp newMatchUp3 = new MatchUp();
                                                                newMatchUp3.TeamName = Program.fifthTeamName;
                                                                newMatchUp3.OpponentTeamName = Program.sixthTeamName;
                                                                Program.matchUpList.Add(newMatchUp3);
                                                                // update played this week
                                                                foreach (Team item in Program.itemList)
                                                                {
                                                                    if (item.TeamName == Program.currentTeamName)
                                                                    {
                                                                        item.PlayedThisWeek++;
                                                                    }
                                                                    if (item.TeamName == Program.secondTeamName)
                                                                    {
                                                                        item.PlayedThisWeek++;
                                                                    }
                                                                }
                                                                Console.WriteLine(Program.currentTeamName + " vs " + Program.thirdTeamName + " Swapped complex reversed");
                                                                Console.WriteLine(Program.secondTeamName + " vs " + Program.forthTeamName + " Swapped complex reversed");
                                                                Console.WriteLine(Program.fifthTeamName + " vs " + Program.sixthTeamName + " Swapped complex reversed");
                                                                complete++;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (complete == 0)
                                    {

                                        // select a team to try
                                        Program.forthTeamName = Program.matchUpList[d].TeamName;
                                        // select their opponent
                                        Program.sixthTeamName = Program.matchUpList[d].OpponentTeamName;
                                        // check that the 2 target teams are not already in a match up
                                        if (Program.matchUpList[d].OpponentTeamName != Program.thirdTeamName)
                                        {
                                            foreach (Team aTeam in Program.itemList)
                                            {
                                                paCounter = 0;
                                                if (Program.forthTeamName == aTeam.TeamName && complete == 0)
                                                {
                                                    foreach (string pa in aTeam.PlayedAgainst)
                                                    {
                                                        if (pa == Program.secondTeamName)
                                                        {
                                                            paCounter++;
                                                        }
                                                    }


                                                    // if second team has found a suitable match, try match the last 2 teams
                                                    if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0 && Program.matchUpList[d].OpponentTeamName != Program.thirdTeamName && Program.forthTeamName == aTeam.TeamName)
                                                    {
                                                        foreach (Team lastTeam in Program.itemList)
                                                        {
                                                            paCounter = 0;
                                                            if (Program.fifthTeamName == aTeam.TeamName && complete == 0)
                                                            {
                                                                foreach (string pa in aTeam.PlayedAgainst)
                                                                {
                                                                    if (pa == Program.sixthTeamName)
                                                                    {
                                                                        paCounter++;
                                                                    }
                                                                }

                                                                // if last teams are suitable, make matches
                                                                if (paCounter <= Program.currentPlayedAgainstCounter && complete == 0)
                                                                {
                                                                    List<MatchUp> matchUpListTemp = new List<MatchUp>();
                                                                    foreach (MatchUp item in Program.matchUpList)
                                                                    {
                                                                        if (item.TeamName != Program.thirdTeamName && item.TeamName != Program.forthTeamName && item.TeamName != Program.fifthTeamName && item.TeamName != Program.sixthTeamName)
                                                                        {
                                                                            matchUpListTemp.Add(item);
                                                                        }
                                                                    }
                                                                    Program.matchUpList = matchUpListTemp;
                                                                    MatchUp newMatchUp2 = new MatchUp();
                                                                    MatchUp newMatchUp1 = new MatchUp();
                                                                    newMatchUp1.TeamName = Program.thirdTeamName;
                                                                    newMatchUp1.OpponentTeamName = Program.currentTeamName;
                                                                    Program.matchUpList.Add(newMatchUp1);
                                                                    newMatchUp2.TeamName = Program.forthTeamName;
                                                                    newMatchUp2.OpponentTeamName = Program.secondTeamName;
                                                                    Program.matchUpList.Add(newMatchUp2);
                                                                    MatchUp newMatchUp3 = new MatchUp();
                                                                    newMatchUp3.TeamName = Program.fifthTeamName;
                                                                    newMatchUp3.OpponentTeamName = Program.sixthTeamName;
                                                                    Program.matchUpList.Add(newMatchUp3);
                                                                    // update played this week
                                                                    foreach (Team item in Program.itemList)
                                                                    {
                                                                        if (item.TeamName == Program.currentTeamName)
                                                                        {
                                                                            item.PlayedThisWeek++;
                                                                        }
                                                                        if (item.TeamName == Program.secondTeamName)
                                                                        {
                                                                            item.PlayedThisWeek++;
                                                                        }
                                                                    }
                                                                    Console.WriteLine(Program.currentTeamName + " vs " + Program.thirdTeamName + " Swapped complex cross reversed");
                                                                    Console.WriteLine(Program.secondTeamName + " vs " + Program.forthTeamName + " Swapped complex cross reversed");
                                                                    Console.WriteLine(Program.fifthTeamName + " vs " + Program.sixthTeamName + " Swapped complex cross reversed");
                                                                    complete++;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    d++;
                                }
                            }
                        }
                        count++;
                    }
                }
            }
            // find which teams still need to be matched up
            // check if each team has played current team
            // once suitable team found, check if each team has played second team
            // if curent and second teams have found suitable opponents, check the if the remaining 2 teams can be matched up
        }
        void SwapTeamsWithBye()
        {
            CheckMatchesMade();
            // if not try swapping with byed teams
            if (Program.matchUpsComplete == 0)
            {
                for (int i = 0; i < ((Program.numberOfGames / 2) - Program.matches); i++)
                {
                    Console.WriteLine("swap bye called");
                    Program.matchMakeList.Clear();
                    // Make a list of available teams
                    foreach (Team value in Program.itemList)
                    {
                        if (value.PlayedThisWeek == 0)
                        {
                            Program.matchMakeList.Add(value);
                        }
                    }
                    Program.currentTeamName = Program.matchMakeList[0].TeamName;
                    Program.secondTeamName = Program.matchMakeList[1].TeamName;
                    // check how many teams were byed this week
                    int count = 0;
                    List<int> gamesPlayedListTemp = new List<int>();
                    foreach (Team value in Program.byeList)
                    {
                        gamesPlayedListTemp.Add(value.GamesPlayed);
                        count++;
                    }
                    // if at least 1 team was byed
                    if (count > 0)
                    {
                        //check how many games both teams have played
                        foreach (Team value in Program.itemList)
                        {
                            if (value.TeamName == Program.currentTeamName)
                            {
                                Program.currentTeamNameGamesPlayed = value.GamesPlayed;
                            }
                            if (value.TeamName == Program.secondTeamName)
                            {
                                Program.secondTeamNameGamesPlayed = value.GamesPlayed;
                            }
                        }
                        // check if current team has played same as any byed teams
                        int complete = 0;
                        int numberInList = 0;
                        foreach (int num in gamesPlayedListTemp)
                        {
                            if (complete == 0 && num == Program.currentTeamNameGamesPlayed)
                            {
                                // check if byed team has played second team
                                int counter = 0;
                                foreach (string value in Program.byeList[numberInList].PlayedAgainst)
                                {
                                    if (value == Program.secondTeamName)
                                    {
                                        counter++;
                                    }
                                }

                                if (counter <= Program.currentPlayedAgainstCounter)
                                {
                                    // give currentteam a bye and a played this week
                                    foreach (Team value in Program.itemList)
                                    {
                                        if (value.TeamName == Program.currentTeamName)
                                        {
                                            value.Byes++;
                                            value.PlayedThisWeek++;
                                            Program.byeList.Add(value);
                                        }
                                    }
                                    // change currentTeamName to new team byeList[numberInList].Teamname
                                    Program.currentTeamName = Program.byeList[numberInList].TeamName;
                                    // remove team from bye list
                                    List<Team> byeListTemp = new List<Team>();
                                    foreach (Team value in Program.byeList)
                                    {
                                        if (value.TeamName != Program.currentTeamName)
                                        {
                                            byeListTemp.Add(value);
                                        }
                                    }
                                    Program.byeList = byeListTemp;
                                    // remove bye from team that is now playing
                                    foreach (Team value in Program.itemList)
                                    {
                                        if (value.TeamName == Program.currentTeamName)
                                        {
                                            value.Byes--;
                                        }
                                    }
                                    // add teams to matchuplist,  update played week and games played
                                    foreach (Team item in Program.itemList)
                                    {
                                        if (item.TeamName == Program.secondTeamName)
                                        {
                                            item.PlayedThisWeek++;
                                        }
                                    }
                                    MatchUp newMatchUp = new MatchUp();
                                    newMatchUp.TeamName = Program.currentTeamName;
                                    newMatchUp.OpponentTeamName = Program.secondTeamName;
                                    complete++;
                                    Console.WriteLine(Program.currentTeamName + " VS " + Program.secondTeamName);
                                    Program.matchUpList.Add(newMatchUp);
                                }
                            }
                            numberInList++;
                        }
                        // if no suitable team was found, try to swap second team with a byed team instead

                        if (complete == 0)
                        {
                            numberInList = 0;
                            foreach (int num in gamesPlayedListTemp)
                            {
                                if (complete == 0 && num == Program.secondTeamNameGamesPlayed)
                                {
                                    // check if byed team has played first team
                                    int counter = 0;
                                    foreach (string value in Program.byeList[numberInList].PlayedAgainst)
                                    {
                                        if (value == Program.currentTeamName)
                                        {
                                            counter++;
                                        }
                                    }

                                    if (counter <= Program.secondTeamNameGamesPlayed)
                                    {
                                        // add secondTeam to byeList
                                        foreach (Team value in Program.itemList)
                                        {
                                            if (value.TeamName == Program.secondTeamName)
                                            {
                                                Program.byeList.Add(value);
                                            }
                                        }
                                        // give secondteam a bye and a played this week
                                        foreach (Team value in Program.itemList)
                                        {
                                            if (value.TeamName == Program.secondTeamName)
                                            {
                                                value.Byes++;
                                                value.PlayedThisWeek++;
                                            }
                                        }
                                        // change secondTeamName to new team from byeList
                                        Program.secondTeamName = Program.byeList[numberInList].TeamName;
                                        // remove bye from team that is now playing
                                        foreach (Team value in Program.itemList)
                                        {
                                            if (value.TeamName == Program.secondTeamName)
                                            {
                                                value.Byes--;
                                            }
                                        }
                                        foreach (Team value in Program.itemList)
                                        {
                                            if (Program.currentTeamName == value.TeamName)
                                            {
                                                value.PlayedThisWeek++;
                                            }
                                        }
                                        // remove team from byeList
                                        List<Team> byeListTemp = new List<Team>();
                                        foreach (Team value in Program.byeList)
                                        {
                                            if (value.TeamName != Program.secondTeamName)
                                            {
                                                byeListTemp.Add(value);
                                            }
                                        }
                                        Program.byeList = byeListTemp;
                                        // add teams to matchuplist,  update played week and games played
                                        MatchUp newMatchUp = new MatchUp();
                                        newMatchUp.TeamName = Program.currentTeamName;
                                        newMatchUp.OpponentTeamName = Program.secondTeamName;
                                        complete++;
                                        Console.WriteLine(Program.currentTeamName + " VS " + Program.secondTeamName);
                                        Program.matchUpList.Add(newMatchUp);
                                    }
                                }
                                numberInList++;
                            }
                        }
                    }
                }
            }
        }
        void PlaceTeams()
        {
            Program.matchUpListFinal.Clear();
            CheckMatchesMade();
            if (Program.matchUpsComplete != 0)
            {

                // make a copy of the settings list that will be altered 
                Program.settingsListTemp = Program.settingsList;
                for (int i = 0; i < (Program.numberOfGames / 2); i++)
                {

                    RefreshSettingsListTemp();
                    SelectGameTime();
                    CheckAvailableForTime();
                    // sort list by times played, select match up with lowest number
                    // copy match to matchUpListFinal
                    // remove matchup from matchUpList
                    if (Program.availableCurrentTime.Count != 0)
                    {
                        var rnd = new Random();
                        var shuffledList = Program.availableCurrentTime.OrderBy(x => rnd.Next()).ToList();
                        int comp = 0;
                        // find if either team has any can't play timeslots, if either team does, place that matchup
                        foreach (MatchUp value in shuffledList)
                        {
                            if (comp == 0)
                            {
                                int team1 = 0;
                                Program.currentTeamName = value.TeamName;
                                Program.secondTeamName = value.OpponentTeamName;
                                foreach (Team team in Program.itemList)
                                {
                                    if (team.TeamName == Program.currentTeamName)
                                    {
                                        foreach (string str in team.CantPlay)
                                        {
                                            team1++;
                                        }
                                    }
                                    if (team.TeamName == Program.secondTeamName)
                                    {
                                        foreach (string str in team.CantPlay)
                                        {
                                            team1++;
                                        }
                                    }
                                }
                                if (team1 != 0)
                                {
                                    comp++;
                                }
                            }
                        }

                        // adds matchup to final list and adds all the rest to an updated list
                        foreach (MatchUp value in Program.matchUpList)
                        {
                            if (value.TeamName == Program.currentTeamName)
                            {
                                value.Time = Program.currentGameTime;
                                Program.matchUpListFinal.Add(value);
                            }
                            if (value.TeamName != Program.currentTeamName)
                            {
                                Program.matchUpListTemp.Add(value);
                            }
                        }

                        Program.matchUpList = Program.matchUpListTemp.ToList<MatchUp>();
                        Program.matchUpListTemp.Clear();

                    }
                }
                // check if all amtches were placed
                if (Program.matchUpListFinal.Count != (Program.numberOfGames / 2))
                {
                    Console.WriteLine("WARNING - NOT ALL MATCHES COULD BE PLACED IN TIMESLOTS");
                }
                // check if a matchup could have happened at another time and switch
                // select last matchup, check if teams can play early or latest game
                int complete = 0;
                if (Program.matchUpListFinal.Count > 2 && Program.settingsList.Count > 2)
                {
                    int counter = 0;
                    Program.secondGameTime = Program.matchUpListFinal[Program.matchUpListFinal.Count - 1].Time;
                    foreach (MatchUp mu in Program.matchUpListFinal)
                    {
                        if (Program.secondGameTime == mu.Time)
                        {
                            complete = 0;
                            Program.currentTeamName = Program.matchUpListFinal[counter].TeamName;
                            Program.secondTeamName = Program.matchUpListFinal[counter].OpponentTeamName;
                            
                            int bust = 0;
                            int bust2 = 0;
                            foreach (Team value in Program.itemList)
                            {
                                if (value.TeamName == Program.currentTeamName)
                                {
                                    foreach (string str in value.CantPlay)
                                    {
                                        if (str == Program.settingsList[0].GameTime)
                                        {
                                            bust++;
                                        }
                                        if (str == Program.settingsList[Program.settingsList.Count - 1].GameTime)
                                        {
                                            bust2++;
                                        }
                                    }
                                }
                                if (value.TeamName == Program.secondTeamName)
                                {
                                    foreach (string str in value.CantPlay)
                                    {
                                        if (str == Program.settingsList[0].GameTime)
                                        {
                                            bust++;
                                        }
                                        if (str == Program.settingsList[Program.settingsList.Count - 1].GameTime)
                                        {
                                            bust2++;
                                        }
                                    }
                                }
                            }

                            // last matchup can play earliest timeslot, check if teams listed then can play 
                            if (bust == 0)
                            {

                                Program.currentGameTime = Program.settingsList[0].GameTime;
                                int count = 0;
                                foreach (MatchUp match in Program.matchUpListFinal)
                                {
                                    
                                    int bust3 = 0;
                                    if (Program.currentGameTime == match.Time && complete == 0)
                                    {
                                        Program.thirdTeamName = match.TeamName;
                                        Program.forthTeamName = match.OpponentTeamName;
                                        if (Program.currentTeamName != Program.thirdTeamName && Program.currentTeamName != Program.forthTeamName)
                                        {
                                            foreach (Team value in Program.itemList)
                                            {
                                                if (Program.thirdTeamName == value.TeamName || Program.forthTeamName == value.TeamName)
                                                {
                                                    foreach (string str in value.CantPlay)
                                                    {
                                                        if (str == Program.matchUpListFinal[counter].Time)
                                                        {
                                                            bust3++;
                                                        }
                                                    }
                                                }
                                            }
                                            // swap times
                                            if (bust3 == 0 && complete == 0)
                                            {
                                                // only do if 1st matchup has less games at the target matchups timeslot
                                                int total = 0;
                                                int total2 = 0;
                                                int total3 = 0;
                                                int total4 = 0;
                                                foreach (Team t in Program.itemList)
                                                {
                                                    if (t.TeamName == Program.currentTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.secondTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total2++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.thirdTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total3++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.forthTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total4++;
                                                            }
                                                        }
                                                    }
                                                }
                                                if ((total3 > total && total3 > total2) || (total4 > total && total4 > total2))
                                                {
                                                    Program.matchUpListFinal[counter].Time = Program.currentGameTime;
                                                    Program.matchUpListFinal[count].Time = Program.secondGameTime;
                                                    complete++;
                                                    Console.WriteLine("swap teams early");
                                                    Console.WriteLine(Program.currentTeamName + " " + Program.thirdTeamName);
                                                }
                                            }
                                        }
                                    }
                                    count++;
                                }
                            }
                            // last matchup can play latest timeslot
                            if (bust2 == 0 && complete == 0)
                            {
                                int count = 0;
                                Program.currentGameTime = Program.settingsList[Program.settingsList.Count - 1].GameTime;
                                foreach (MatchUp match in Program.matchUpListFinal)
                                {
                                    int bust3 = 0;
                                    
                                    if (Program.currentGameTime == match.Time && complete == 0)
                                    {
                                        Program.thirdTeamName = match.TeamName;
                                        Program.forthTeamName = match.OpponentTeamName;
                                        if (Program.currentTeamName != Program.thirdTeamName && Program.currentTeamName != Program.forthTeamName)
                                        {
                                            foreach (Team value in Program.itemList)
                                            {
                                                if (Program.thirdTeamName == value.TeamName || Program.forthTeamName == value.TeamName)
                                                {
                                                    foreach (string str in value.CantPlay)
                                                    {
                                                        if (str == Program.matchUpListFinal[ counter].Time)
                                                        {
                                                            bust3++;
                                                        }
                                                    }
                                                }
                                            }
                                            // swap times
                                            if (bust3 == 0 && complete == 0)
                                            {
                                                // only do if 1st matchup has less games at the target matchups timeslot
                                                int total = 0;
                                                int total2 = 0;
                                                int total3 = 0;
                                                int total4 = 0;
                                                foreach (Team t in Program.itemList)
                                                {
                                                    if (t.TeamName == Program.currentTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.secondTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total2++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.thirdTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total3++;
                                                            }
                                                        }
                                                    }
                                                    if (t.TeamName == Program.forthTeamName)
                                                    {
                                                        foreach (string s in t.TimesPlayed)
                                                        {
                                                            if (s == Program.currentGameTime)
                                                            {
                                                                total4++;
                                                            }
                                                        }
                                                    }
                                                }
                                                if ((total3 > total && total3 > total2) || (total4 > total && total4 > total2))
                                                {
                                                    Program.matchUpListFinal[counter].Time = Program.currentGameTime;
                                                    Program.matchUpListFinal[count].Time = Program.secondGameTime;
                                                    complete++;
                                                    Console.WriteLine("swap teams late");
                                                    Console.WriteLine(Program.currentTeamName + " " + Program.thirdTeamName);
                                                } 
                                            }
                                        }
                                    }
                                    count++; 
                                }
                            }
                        }
                        counter++;  
                    }
                }
            }
            else { }
        }
        void SelectGameTime()
        {
            for (int i = 0; Program.numberOfGamesToRemove > i; Program.numberOfGamesToRemove--)
            {
                int settingsLength = 0;
                foreach (Settings setting in Program.settingsListTemp)
                {
                    settingsLength++;
                }
                Program.settingsListTemp[(settingsLength - 1)].NumberOfGameAtThatTime--;
                RefreshSettingsListTemp();
            }
            // alternate early and late games ( maybe check if there is a majority one way or the other and then set the logically time first every time.)
            // set currrent game time and remove 1 game from numberOfGamesAtThatTime
            if (Program.alternate == 0)
            {
                // find the earliest timeslot
                Program.currentGameTime = Program.settingsListTemp[0].GameTime;
                Program.settingsListTemp[0].NumberOfGameAtThatTime--;
                Program.alternate++;
            }
            else
            {
                // find the latest timeslot
                int settingsLength = 0;
                foreach (Settings setting in Program.settingsListTemp)
                {
                    settingsLength++;
                }
                Program.currentGameTime = Program.settingsListTemp[(settingsLength - 1)].GameTime;
                Program.settingsListTemp[(settingsLength - 1)].NumberOfGameAtThatTime--;
                Program.alternate = 0;
            }
        }
        void RefreshSettingsListTemp()
        {
            List<Settings> settingsListTemp2 = new List<Settings>();
            foreach (Settings setting in Program.settingsListTemp)
            {
                if (setting.NumberOfGameAtThatTime != 0)
                {
                    settingsListTemp2.Add(setting);
                }
            }
            Program.settingsListTemp = settingsListTemp2;
        }
        void CheckMatchesMade()
        {
            Program.matches = 0;
            foreach (MatchUp value in Program.matchUpList)
            {
                Program.matches++;
            }
            if (Program.matches == (Program.numberOfGames / 2))
            {
                Program.matchUpsComplete++;
            }
        }
        void CheckPlayedAt()
        {
            // checks how many games at current timeslot have been played by each team
            foreach (Team value in Program.itemList)
            {
                value.PlayedAt = 0;
                foreach (string time in value.TimesPlayed)
                {
                    if (time == Program.currentGameTime)
                    {
                        value.PlayedAt++;
                    }
                }
            }
        }
        void CheckAvailableForTime()
        {
            // reset variables
            foreach (MatchUp value in Program.matchUpList)
            {
                value.TimesPlayed = 0;
            }
            Program.availableCurrentTime.Clear();
            // check each matchup to see if they can play at the current timeslot, 
            // record how many times the teams have played at this timeslot combined
            // add teams to new list if they both are available
            foreach (MatchUp item in Program.matchUpList)
            {
                int count = 0;
                int count2 = 0;
                foreach (Team value in Program.itemList)
                {
                    if (item.TeamName == value.TeamName)
                    {
                        foreach (string time in value.CantPlay)
                        {
                            if (time == Program.currentGameTime)
                            {
                                count++;
                            }
                        }
                        foreach (string time in value.TimesPlayed)
                        {
                            if (time == Program.currentGameTime)
                            {
                                item.TimesPlayed++;
                            }
                        }
                    }
                    if (item.OpponentTeamName == value.TeamName)
                    {
                        foreach (string time in value.CantPlay)
                        {
                            if (time == Program.currentGameTime)
                            {
                                count2++;
                            }
                        }
                        foreach (string time in value.TimesPlayed)
                        {
                            if (time == Program.currentGameTime)
                            {
                                item.TimesPlayed++;
                            }
                        }
                    }
                }
                if (count == 0 && count2 == 0)
                {
                    Program.availableCurrentTime.Add(item);
                }
            }
        }
        void OutputMatches()
        {

            Console.WriteLine();
            Console.WriteLine("WEEK " + Program.week);
            List<MatchUp> matchUpListFinalSorted = Program.matchUpListFinal.OrderBy(o => o.Time).ToList();
            foreach (MatchUp value in matchUpListFinalSorted)
            {
                Console.WriteLine(value.Time + " " + value.TeamName + " vs " + value.OpponentTeamName);
            }
        }
        void SetTimesPlayed()
        {
            foreach (MatchUp matchup in Program.matchUpListFinal)
            {
                foreach (Team value in Program.itemList)
                {
                    if (value.TeamName == matchup.TeamName)
                    {
                        value.TimesPlayed.Add(matchup.Time);
                    }
                    if (value.TeamName == matchup.OpponentTeamName)
                    {
                        value.TimesPlayed.Add(matchup.Time);
                    }
                }
            }
        }
        void CheckToIncreaseCurrentPlayedAgainstCounter()
        {
            int count = 1;
            if (Program.numberOfTeams > 14)
            {
                count = 2;
            }
            if (Program.week < (Program.numberOfTeams - count))
            {
                Program.currentPlayedAgainstCounter = 0;
            }
            int counter = 1;
            for (int i = 0; i < 10; i++)
            {
                if (Program.week >= (Program.numberOfTeams - count) * counter && Program.week < (Program.numberOfTeams - count) * (counter + 1))
                {
                    Program.currentPlayedAgainstCounter = counter;
                }
                counter++;
            }
        }

        //void RemovePlayedThisWeekProp()
        //{
        //    // reset all teams playedThisWeek properties except teams that need byes
        //    foreach (Team value in Program.itemList)
        //    {
        //        value.PlayedThisWeek = 0;
        //    }
        //}

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            AddTeam addTeam = new AddTeam();
            addTeam.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            RemoveTeam removeTeam = new RemoveTeam();
            removeTeam.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            Program.matrix.Clear();
            LoadTeamData.loadTeamData();
            ReadGameTimesFile.readGameTimesFile();
            GetNumberOfTeams();
            StatisticsPAForm statFrm = new StatisticsPAForm();
            statFrm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ReadGameTimesFile.readGameTimesFile();
            if (Program.week > 0)
            {
                Program.weekStats = Program.week;
                PreviousMatchUpsForm previousMatchUpForm = new PreviousMatchUpsForm();
                previousMatchUpForm.ShowDialog();
            }
            else
            {
                ErrorPreviousForm errorPreviousForm = new ErrorPreviousForm();
                errorPreviousForm.ShowDialog();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ConfirmNewSeason confirmNewSeason = new ConfirmNewSeason();
            confirmNewSeason.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            GiveBye giveBye = new GiveBye();
            giveBye.ShowDialog();
        }
        // undo last week matchups
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (File.Exists(@Program.filePath + Program.currentSport + "\\" + Program.currentLeague + "\\backup\\teams.csv"))
            {
                ConfirmUndo confrimUndo = new ConfirmUndo();
                confrimUndo.ShowDialog();
            }
            else
            {
                // cannot undo matches, no files in backup
                ErrorUndoForm errorUndoForm = new ErrorUndoForm();
                errorUndoForm.ShowDialog();
            }

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            RenameTeam renameTeam = new RenameTeam();
            renameTeam.ShowDialog();
        }
        // remove bye button
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            LoadTeamData.loadTeamData();
            RemoveBye removeBye = new RemoveBye();
            removeBye.ShowDialog();

        }
        // add matchup button
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            AddMatchUp addMatchUp = new AddMatchUp();
            addMatchUp.ShowDialog();
        }
        // remove match up button
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            ReadGameTimesFile.readGameTimesFile();
            if (Program.week > 0)
            {
                Program.weekStats = Program.week;
                RemoveMatchUp removeMatchUp = new RemoveMatchUp();
                removeMatchUp.ShowDialog();
            }
            else
            {
                ErrorPreviousForm errorPreviousForm = new ErrorPreviousForm();
                errorPreviousForm.ShowDialog();
            }
        }

        

        
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.MakeMatches2;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Resources.MakeMatches1;
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.Image = Resources.AddTeam2;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Image = Resources.AddTeam1;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.NewSeason2;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.NewSeason1;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.Settings2;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.Settings1;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.Image = Resources.EditTeam2;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Image = Resources.EditTeam1;
        }

        private void pictureBox11_MouseEnter(object sender, EventArgs e)
        {
            pictureBox11.Image = Resources.UndoMatchUps2;
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.Image = Resources.UndoMatchUps1;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.Statistics2;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.Statistics1;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Resources.RemoveTeam2;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = Resources.RemoveTeam1;
        }

        private void pictureBox12_MouseEnter(object sender, EventArgs e)
        {
            pictureBox12.Image = Resources.RenameTeam2;
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            pictureBox12.Image = Resources.RenameTeam1;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.ViewPreviousMatches2;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.ViewPreviousMatches1;
        }

        private void pictureBox10_MouseEnter(object sender, EventArgs e)
        {
            pictureBox10.Image = Resources.GiveBye2;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.Image = Resources.GiveBye1;
        }

        private void pictureBox13_MouseEnter(object sender, EventArgs e)
        {
            pictureBox13.Image = Resources.RemoveBye2;
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Image = Resources.RemoveBye1;
        }

        private void pictureBox14_MouseEnter(object sender, EventArgs e)
        {
            pictureBox14.Image = Resources.AddMatchup2;
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            pictureBox14.Image = Resources.AddMatchup1;
        }

        private void pictureBox15_MouseEnter(object sender, EventArgs e)
        {
            pictureBox15.Image = Resources.RemoveMatchUp2;
        }

        private void pictureBox15_MouseLeave(object sender, EventArgs e)
        {
            pictureBox15.Image = Resources.RemoveMatchUp1;
        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.Image = Resources.Done2;
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.Image = Resources.Done1;
        }

    }
}
