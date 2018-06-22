using DubuqueCodeCamp.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DubuqueCodeCamp.Scheduler
{
    /// <summary>
    /// Class for performing various read and write operations on the Database for the Scheduler project
    /// </summary>
    public class DatabaseOperations
    {
        private static readonly DCCKellyDatabase DATABASE = new DCCKellyDatabase();

        /// <summary>
        /// Create a proposed Schedule and save it to the database based on elements already in the database
        /// </summary>
        /// <param name="eventDate">Date of the event that the schedule will be created for</param>
        public static bool CreateProposedSchedule(DateTime eventDate)
        {
            try
            {
                // Ensure the Event Date does not have an unnecessary time added
                eventDate = eventDate.Date;

                // Get the talkIDs and order them by decreasing count
                var cachedTalks = DATABASE.Talks.Where(talk => talk.DateGiven == eventDate).Select(talk => talk.ID).ToList();
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
                {
                    MessageBox.Show(
                        "There aren't enough rooms and/or sessions to create a full schedule for all the talks available.",
                        "Not Enough Rooms", MessageBoxButton.OK);
                        return false;
                }

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

                DATABASE.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Get the existing schedule entries for the given date from the database
        /// </summary>
        /// <param name="eventDate">Date of the event to query for</param>
        /// <returns>List of <see cref="ProposedSchedule"/></returns>
        public static List<ProposedSchedule> GetProposedSchedule(DateTime eventDate)
        {
            return (from sched in DATABASE.ProposedSchedules
                    join session in DATABASE.Sessions on sched.SessionID equals session.ID
                    where session.SessionDate == eventDate
                    select sched).ToList();
        }

        /// <summary>
        /// Get the existing sessions for the given date from the database
        /// </summary>
        /// <param name="eventDate">Date of the event to query for</param>
        /// <returns>List of <see cref="Session"/></returns>
        public static List<Session> GetExistingSessions(DateTime eventDate)
        {
            return (from session in DATABASE.Sessions
                    where session.SessionDate == eventDate
                    select session).ToList();
        }

        /// <summary>
        /// Get the existing talks for the given date from the database
        /// </summary>
        /// <param name="eventDate">Date of the event to query for</param>
        /// <returns>List of <see cref="Talk"/></returns>
        public static List<Talk> GetExistingTalks(DateTime eventDate)
        {
            return (from talk in DATABASE.Talks
                    where talk.DateGiven == eventDate
                    select talk).ToList();
        }

        /// <summary>
        /// Get the existing schedule entries for the given date and map it into a format the UI can read
        /// </summary>
        /// <param name="eventDate">Date of the event to query for</param>
        /// <returns>List of <see cref="TalkSession"/></returns>
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

        public static bool SaveSession(Session newSession)
        {
            // If the database doesn't have any existing sessions matching the new one, save to the database
            if (!DATABASE.Sessions.Any(session => session.SessionDate == newSession.SessionDate &&
                                                  session.TimeStart == newSession.TimeStart &&
                                                  session.TimeEnd == newSession.TimeEnd))
            {
                DATABASE.Sessions.InsertOnSubmit(newSession);
                DATABASE.SubmitChanges();

                return true;
            }

            // Otherwise, tell the user one exists and navigate away as usual
            MessageBox.Show("A session with the same date and start and end times already exists in the database. Canceling",
                "Canceling Save",
                MessageBoxButton.OK);
            return false;
        }

        public static void RemoveSession(Session removeSession)
        {
            var removedItem = DATABASE.Sessions.Single(session => session.SessionDate == removeSession.SessionDate &&
                                                                  session.TimeStart == removeSession.TimeStart &&
                                                                  session.TimeEnd == removeSession.TimeEnd);
            DATABASE.Sessions.DeleteOnSubmit(removedItem);
        }

        public static List<TalkInformation> GetTalkInformation(DateTime eventDate)
        {
            return (from talk in DATABASE.Talks
                    where talk.DateGiven == eventDate
                    let speaker = (from s in DATABASE.Speakers
                                   where s.ID == talk.SpeakerID
                                   select s).Single()
                    select new TalkInformation
                    {
                        TalkDate = talk.DateGiven,
                        TalkTitle = talk.Title,
                        TalkSummary = talk.Summary,
                        SpeakerFirstName = speaker.FirstName,
                        SpeakerLastName = speaker.LastName
                    }).ToList();
        }

        public static bool SaveTalk(TalkInformation newTalk)
        {
            var speaker = DATABASE.Speakers.SingleOrDefault(s =>
                s.FirstName == newTalk.SpeakerFirstName && s.LastName == newTalk.SpeakerLastName);
            if (speaker == null)
            {
                // Warn the user that no speaker exists with that name
                MessageBox.Show($"No speaker exists with the name {newTalk.SpeakerFirstName} {newTalk.SpeakerLastName}",
                    "Canceling Save",
                    MessageBoxButton.OK);
                return false;
            }

            // If the database doesn't have any existing sessions matching the new one, save to the database
            if (!DATABASE.Talks.Any(talk => talk.DateGiven == newTalk.TalkDate &&
                                            talk.Title == newTalk.TalkTitle &&
                                            talk.Summary == newTalk.TalkSummary))
            {
                DATABASE.Talks.InsertOnSubmit(new Talk
                {
                    DateGiven = newTalk.TalkDate,
                    Title = newTalk.TalkTitle,
                    Summary = newTalk.TalkSummary,
                    SpeakerID = speaker.ID,
                    UpdateTime = DateTime.Now,
                    DiagnosticInformation = new StackTrace().ToString()
                });
                DATABASE.SubmitChanges();
                return true;
            }

            return false;
        }

        public static void RemoveTalk(TalkInformation removeTalk)
        {
            var speakerID = DATABASE
                .Speakers.Single(s => s.FirstName == removeTalk.SpeakerFirstName && s.LastName == removeTalk.SpeakerLastName).ID;

            var removedItem = DATABASE.Talks.Single(talk => talk.DateGiven == removeTalk.TalkDate &&
                                                           talk.Title == removeTalk.TalkTitle &&
                                                           talk.Summary == removeTalk.TalkSummary &&
                                                           talk.SpeakerID == speakerID);
            DATABASE.Talks.DeleteOnSubmit(removedItem);
            DATABASE.SubmitChanges();
        }
    }
}