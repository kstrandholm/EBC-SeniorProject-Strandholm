using System.Collections.Generic;
using System.Linq;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    public class DetermineSchedule
    {
        private readonly DCCKellyDatabase _database = new DCCKellyDatabase();

        public void GetProposedSchedule()
        {
            var cachedTalks = _database.Talks.Select(talk => talk.ID).ToList();
            var interestCount = (from talkID in _database.Talks.Select(talk => talk.ID).ToList()
                                 let count = _database.TalkInterest.Count(interest => interest.TalkID == talkID)
                                 select (talkID, count)).ToList();
        }
    }
}
