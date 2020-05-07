using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamsFile = "TeamModels.csv";

        /// TODO - Make the CreatePrize method actually save to the text files.
        /// <summary>
        /// Saves a new prize to the text files.
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns>The prize information, including the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Order the List by Id, find max Id to get new Id (max + 1)
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Add the new record with the new ID (max + 1)
            model.Id = currentId;
            prizes.Add(model);

            // Convert the prizes to list<string>
            // Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            // Order the List by Id, find max Id to get new Id (max + 1)
            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Add the new record with the new ID (max + 1)
            model.Id = currentId;
            people.Add(model);

            // Convert the prizes to list<string>
            // Save the list<string> to the text file
            people.SaveToPersonFile(PeopleFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> team = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            // Order the List by Id, find max Id to get new Id (max + 1)
            int currentId = 1;

            if (team.Count > 0)
            {
                currentId = team.OrderByDescending(x => x.Id).First().Id + 1;
            }

            // Add the new record with the new ID (max + 1)
            model.Id = currentId;
            team.Add(model);

            // Convert the prizes to list<string>
            // Save the list<string> to the text file
            team.SaveToTeamFile(TeamsFile);

            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            throw new NotImplementedException();
        }
    }
}
