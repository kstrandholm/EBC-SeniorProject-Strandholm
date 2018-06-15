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

            var cachedRooms = _database.Rooms.ToList();
            var cachedSessions = _database.Sessions.ToList();

            // If there are no sessions, we can't create a proposed schedule
            if (!cachedSessions.Any())
                return;

            (from room in cachedRooms
            from session in cachedSessions
            select new )
        }
    }
}
