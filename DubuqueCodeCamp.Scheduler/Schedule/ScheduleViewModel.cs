using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    public class ScheduleViewModel : BindableBase
    {
        private readonly DCCKellyDatabase _database = new DCCKellyDatabase();

        private List<ProposedSchedule> _schedules;
        public List<ProposedSchedule> Scheudles
        {
            get { return _schedules; }
            set { SetProperty(ref _schedules, value); }
        }

        public ScheduleViewModel()
        {
        }

        public List<ProposedSchedule> GetProposedSchedule(DateTime eventDate)
        {
            // Ensure the Event Date does not have an unnecessary time added
            eventDate = eventDate.Date;

            return (from sched in _database.ProposedSchedules
                    join session in _database.Sessions on sched.SessionID equals session.ID
                    where session.SessionDate == eventDate
                    select sched).ToList();
        }

        public List<Session> GetExistingSessions(DateTime eventDate)
        {
            // Ensure the Event Date does not have an unnecessary time added
            eventDate = eventDate.Date;

            return (from session in _database.Sessions
                    where session.SessionDate == eventDate
                    select session).ToList();
        }

        public void CreateProposedSchedule(DateTime eventDate)
        {
            // Ensure the Event Date does not have an unnecessary time added
            eventDate = eventDate.Date;

            var cachedTalks = _database.Talks.Select(talk => talk.ID).ToList();
            var talks = (from talkID in cachedTalks
                         let count = _database.TalkInterest.Count(interest => interest.TalkID == talkID)
                         orderby count descending
                         select talkID).ToList();

            // If there are no sessions, we can't create a proposed schedule
            if (!_database.Sessions.Any())
                return;

            var schedulesForDate = (from sched in _database.ProposedSchedules
                                    join session in _database.Sessions on sched.SessionID equals session.ID
                                    where session.SessionDate == eventDate
                                    select sched).ToList();

            // If there are any existing schedules for this date, warn the user that creating a new one will overwrite the existing one
            if (schedulesForDate.Any())
            {
                var result = MessageBox.Show("A proposed schedule already exists. Generating a new one will overwrite the old. Continue?",
                    "Existing Proposed Schedule", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                    return;
            }

            // Combine the Rooms and Sessions to create each unique combination
            var roomSessions = (from room in _database.Rooms
                                from session in _database.Sessions
                                select new { room, session })
                .OrderByDescending(rs => rs.room.Capacity) // Largest rooms first
                .ToList();

            // If the number of roomSessions is not equal to the number of talks in interestCount we can't create an accurate schedule
            if (roomSessions.Count != talks.Count)
                return;

            for (var i = 0; i < roomSessions.Count; i++)
            {
                var roomSession = roomSessions[i];
                var newSchedule = new ProposedSchedule
                {
                    RoomID = roomSession.room.ID,
                    SessionID = roomSession.session.ID,
                    TalkID = talks[i],
                    UpdateTime = DateTime.Now,
                    DiagnosticInformation = $"Adding Proposed Schedule for event on {eventDate}"
                };
                _database.ProposedSchedules.InsertOnSubmit(newSchedule);
            }
        }
    }
}
