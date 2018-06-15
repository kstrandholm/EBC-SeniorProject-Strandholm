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
            var interestCount = (from talkID in cachedTalks
                                 let count = _database.TalkInterest.Count(interest => interest.TalkID == talkID)
                                 select (talkID, count)).OrderByDescending(order => order.Item2).ToList();

            //var rooms = _database.
        }
    }
}
