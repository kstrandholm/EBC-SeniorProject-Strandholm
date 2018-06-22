using System;
using System.Collections.Generic;
using System.Diagnostics;
using DubuqueCodeCamp.DatabaseConnection;
using System.Linq;

namespace DubuqueCodeCamp.Registration
{
    /// <summary>
    /// Class for performing various read and write operations on the Database for the Registration project
    /// </summary>
    public class DatabaseOperations
    {
        private static readonly DCCKellyDatabase DATABASE = new DCCKellyDatabase();
        
        /// <summary>
        /// Get a list of the Talks in the database and map them to a list of ChosenTalks
        /// </summary>
        /// <returns>List of talks that can be chosen</returns>
        public static List<ChosenTalk> GetChosenTalks()
        {
            const bool NOT_CHOSEN = false;

            return (from talk in DATABASE.Talks
                               where talk.DateGiven == DateTime.Today
                               let speaker = (from s in DATABASE.Speakers
                                              where s.ID == talk.SpeakerID
                                              select s).Single()
                               select new ChosenTalk
                               {
                                   TalkID = talk.ID,
                                   TalkTitle = talk.Title,
                                   TalkSummary = talk.Summary,
                                   SpeakerFirstName = speaker.FirstName,
                                   SpeakerLastName = speaker.LastName,
                                   Chosen = NOT_CHOSEN
                               }).ToList();
        }

        /// <summary>
        /// Save the given Registration Information to the database
        /// </summary>
        /// <param name="registration">RegistrationInformation record to save</param>
        public static void SaveRegistration(RegistrationInformation registration)
        {
            SaveRegistrant(registration);

            SaveTalkInterest(registration);
        }

        private static void SaveRegistrant(IRegistrant registration)
        {
            var registrant = new Registrant
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                EmailAddress = registration.EmailAddress,
                Occupation = registration.Occupation,
                BirthDate = registration.BirthDate
            };
            DATABASE.Registrants.InsertOnSubmit(registrant);
            DATABASE.SubmitChanges();
        }

        private static void SaveTalkInterest(RegistrationInformation registration)
        {
            var registrantID = DATABASE
                .Registrants.Single(reg => reg.FirstName == registration.FirstName && reg.LastName == registration.LastName).ID;

            var talkInterests = (from talk in registration.ChosenTalks
                                 where talk.Chosen
                                 select new TalkInterest
                                 {
                                     InterestedRegistrantID = registrantID,
                                     TalkID = talk.TalkID,
                                     UpdateTime = DateTime.Now,
                                     DiagnosticInformation = new StackTrace().ToString()
                                 }).ToList();

            DATABASE.TalkInterest.InsertAllOnSubmit(talkInterests);

            DATABASE.SubmitChanges();
        }
    }
}
