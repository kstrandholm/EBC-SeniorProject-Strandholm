using DubuqueCodeCamp.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DubuqueCodeCamp.Scheduler
{
    public class DetermineSchedule
    {
        private readonly DCCKellyDatabase _database = new DCCKellyDatabase();

        public void GetProposedSchedule(DateTime eventDate)
        {
            var cachedTalks = _database.Talks.Select(talk => talk.ID).ToList();
            var interestCount = (from talkID in cachedTalks
                                 let count = _database.TalkInterest.Count(interest => interest.TalkID == talkID)
                                 orderby count descending
                                 select talkID).ToList();

            // If there are no sessions, we can't create a proposed schedule
            if (!_database.Sessions.Any())
                return;

            // If there are any existing schedules for this date, warn the user that creating a new one will overwrite the existing one
            if (_database.ProposedSchedules.Any(schedule => schedule.Session.TimeStart.Date == eventDate))
            {
                var result = MessageBox.Show("A proposed schedule already exists. Generating a new one will overwrite the old. Continue?",
                    "Existing Proposed Schedule", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
            }

            // Combine the Rooms and Sessions to create each unique combination
            var roomSessions = (from room in _database.Rooms
                                from session in _database.Sessions
                                select new {room, session})
                .OrderByDescending(rs => rs.room.Capacity) // Largest rooms first
                .ToList();

            // If the number of roomSessions is not equal to the number of talks in interestCount we can't create an accurate schedule
            if (roomSessions.Count != interestCount.Count)
                return;

            for (var i = 0; i < roomSessions.Count; i++)
            {
                var roomSession = roomSessions[i];
                var newSchedule = new ProposedSchedule
                {
                    Room = roomSession.room,
                    Session = roomSession.session,
                    Talk = _database.Talks.Single(talk => talk.ID == interestCount[i]),
                    UpdateTime = DateTime.Now,
                    DiagnosticInformation = $"Adding Proposed Schedule for event on {eventDate}"
                };
                _database.ProposedSchedules.InsertOnSubmit(newSchedule);
            }

        }
    }
}
