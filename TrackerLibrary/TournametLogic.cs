using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournametLogic
    {

        public static void CreateRounds(TournamentModel model)
        {
            // Order team list randomly
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);

            // Check if team count is 2^n - if not, add in byes (empty matchup entry)
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            // Create our first round of matchups and add to TournamentModel
            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            // Create every round after that - 8 matchups - 4 matchups - 2 matchups - 1 matchup
            CreateOtherRounds(model, rounds);
        }

        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            // https://stackoverflow.com/questions/273313/randomize-a-listt
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private static int FindNumberOfRounds(int numberOfTeams)
        {
            int output = 1;
            int val = 2;

            while (val < numberOfTeams)
            {
                output++;
                val *= 2;
            }

            return output;
        }

        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            return totalTeams - numberOfTeams;
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes > 0 || curr.Entries.Count > 1)
                {
                    curr.MatchupRound = 1;
                    output.Add(curr);
                    curr = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return output;
        }

        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currMatchup.Entries.Count > 1)
                    {
                        currMatchup.MatchupRound = round;
                        currRound.Add(currMatchup);
                        currMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currRound);
                previousRound = currRound;
                currRound = new List<MatchupModel>();
                round++;
            }
        }
    }
}
