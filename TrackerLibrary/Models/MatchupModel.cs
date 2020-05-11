using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one match in the tournament.
    /// Ex. For a match where team 3 won against team 4
    ///    MatchupModel
    ///    - Id : 10
    ///    - Entries
    ///      ∟ MatchupEntryModel
    ///        - Id: 1
    ///        - MatchupId: 10
    ///        - TeamCompetingId : 3
    ///      ∟ MatchupEntryModel
    ///        - Id: 2
    ///        - MatchupId: 10
    ///        - TeamCompetingId : 4
    ///    - WinnerId: 3
    ///    - MatchupRound : 1
    /// </summary>
    public class MatchupModel
    {
        /// <summary>
        /// The unique identifier for the matchup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The set of teams that were involved in this match.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        
        /// <summary>
        /// The winner of the match.
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Which round this match is a part of.
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
