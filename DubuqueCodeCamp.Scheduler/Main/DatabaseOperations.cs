using DubuqueCodeCamp.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DubuqueCodeCamp.Scheduler
{
    public class DatabaseOperations
    {
        private static readonly DCCKellyDatabase DATABASE = new DCCKellyDatabase();

        /// <summary>
        /// Create a proposed Schedule and save it to the database based on elements already in the database
        /// </summary>
        /// <param name="eventDate">Date of the event that the schedule will be created for</param>
        public static bool CreateProposedSchedule(DateTime eventDate)
        {
            // Ensure the Event Date does not have an unnecessary time added
            eventDate = eventDate.Date;

            // TODO: filter on dategiven once database null nonsense taken care of
            // Get the talkIDs and order them by decreasing count
            var cachedTalks = DATABASE.Talks.Select(talk => talk.ID).ToList();
            var talks = (from talkID in cachedTalks
                         let count = DATABASE.TalkInterest.Count(interest => interest.TalkID == talkID)
                         orderby count descending
                         select talkID).ToList();

            // Get the existing ProposedSchedule entries
            var schedulesForDate = (from sched in DATABASE.ProposedSchedules
                                    join session in DATABASE.Sessions on sched.SessionID equals session.ID
                                    where session.SessionDate == eventDate
                                    select sched).ToList();

            // If there are any existing schedules for this date, warn the user that creating a new one will overwrite the existing one
            if (schedulesForDate.Any())
            {
                var result = MessageBox.Show("A proposed schedule already exists. Generating a new one will overwrite the old. Continue?",
                    "Existing Proposed Schedule", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.Cancel)
                    return false;

                // Delete the existing schedule for the eventDate to overwrite them
                DATABASE.ProposedSchedules.DeleteAllOnSubmit(schedulesForDate);
            }

            // Combine the Rooms and Sessions to create each unique combination
            var roomSessions = (from room in DATABASE.Rooms
                                from session in DATABASE.Sessions.Where(session => session.SessionDate == eventDate)
                                select new { room, session })
                .OrderByDescending(rs => rs.room.Capacity) // Largest rooms first
                .ToList();

            // We need at least as many roomSessions as there are talks to create a schedule
            if (roomSessions.Count < talks.Count)
                return false;

            // Add the talks by decreasing interest into the rooms/sessions by decreasing capacity to create the Proposed Schedule
            for (var i = 0; i < roomSessions.Count && i < talks.Count; i++)
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
                DATABASE.ProposedSchedules.InsertOnSubmit(newSchedule);
            }

            // TODO: implement a try catch to verify actually saved
            // TODO: give user feedback if not saved/created
            DATABASE.SubmitChanges();
            return true;
        }

        public static List<ProposedSchedule> GetProposedSchedule(DateTime eventDate)
        {
            return (from sched in DATABASE.ProposedSchedules
                    join session in DATABASE.Sessions on sched.SessionID equals session.ID
                    where session.SessionDate == eventDate
                    select sched).ToList();
        }

        public static List<Session> GetExistingSessions(DateTime eventDate)
        {
            return (from session in DATABASE.Sessions
                    where session.SessionDate == eventDate
                    select session).ToList();
        }

        public static List<Talk> GetExistingTalks(DateTime eventDate)
        {
            // TODO: remove null check once database null nonsense is taken care of
            return (from talk in DATABASE.Talks
                    where talk.DateGiven == eventDate || talk.DateGiven == null
                    select talk).ToList();
        }

        public static List<TalkSession> GetMappedTalkSessions(DateTime eventDate)
        {
            var proposedSchedule = GetProposedSchedule(eventDate);

            return (from schedule in proposedSchedule
                    let session = DATABASE.Sessions.Single(s => s.ID == schedule.SessionID)
                    let room = DATABASE.Rooms.Single(r => r.ID == schedule.RoomID)
                    let talk = DATABASE.Talks.Single(t => t.ID == schedule.TalkID)
                    let speaker = DATABASE.Speakers.Single(s => s.ID == talk.SpeakerID)
                    select new TalkSession
                    {
                        SessionDate = session.SessionDate,
                        SessionStartTime = session.TimeStart,
                        SessionEndTime = session.TimeEnd,
                        SessionLengthMinutes = session.CalcLengthMinutes,
                        RoomName = room.RoomName,
                        RoomCapacity = room.Capacity,
                        Venue = room.Venue,
                        TalkTitle = talk.Title,
                        TalkSummary = talk.Summary,
                        SpeakerFirstName = speaker.FirstName,
                        SpeakerLastName = speaker.LastName
                    }).ToList();
        }
    }
}