using System.Collections.Generic;
using System.Linq;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    public class DetermineSchedule
    {
        private DCCKellyDatabase _database = new DCCKellyDatabase();

        public void GetProposedSchedule()
        {
            List<(int talk, int interestAmount)> interestCount = _database.Talks.Select(talk => new (int talk, int interestAmount){(talk.ID, _database.TalkInterest.Count(interest => interest.TalkID == talk.ID))}).ToList();
        }
    }
}
