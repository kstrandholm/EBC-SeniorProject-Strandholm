using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DubuqueCodeCamp.DatabaseConnection;
using Serilog;

namespace DubuqueCodeCamp.Downloader
{
    /// <summary>
    /// Class that handles writing records to the database
    /// </summary>
    public class WriteToDatabase
    {
        private readonly ILogger _logger;
        private readonly DCCKellyDatabase _database;

        /// <summary>
        /// Constructor for the WriteToDatabase class
        /// </summary>
        public WriteToDatabase()
        {
            _logger = LoggingInitializer.GetLogger();
            _database = new DCCKellyDatabase();
        }

        /// <summary>
        /// Save the given registrantInformation to the database
        /// </summary>
        /// <param name="registrantInformation"></param>
        public void WriteDownloadRecords(IReadOnlyCollection<RegistrantInformation> registrantInformation)
        {
            using (_database)
            {
                var databaseType = _database.GetType().ToString();

                WriteRegistrantInformation(registrantInformation, databaseType);

                // Now map the registrant's Talk Interests
                WriteTalkInterests(registrantInformation, databaseType); 
            }
        }

        private void WriteRegistrantInformation(IReadOnlyCollection<RegistrantInformation> registrantInformation, string databaseType)
        {
            const string REGISTRANTS = nameof(_database.Registrants);

            var databaseRegistrants = _database.Registrants;

            // Convert the RegistrantInformation from the parser into a format that can be saved to the database
            var newRegistrants = MapRegistrantInformationToRegistrantTable(registrantInformation);

            // TODO: Get Equality overrides to work
            // If the record is not a duplicate of what is already in the database, add it to the database
            var uniqueRegistrants = newRegistrants.Where(newReg => !databaseRegistrants.Any(dataReg =>
                dataReg.FirstName == newReg.FirstName && dataReg.LastName == newReg.LastName &&
                string.Equals(dataReg.EmailAddress, newReg.EmailAddress))).ToList();

            // Try using the new Equals operator I implemented - can't figure out how to get this to work
            //var uniqueRegistrants = newRegistrants.Where(newReg => !databaseRegistrants.Any(dataReg =>
            //    dataReg == newReg)).ToList();

            if (uniqueRegistrants.Any())
            {
                try
                {
                    _logger.Information($"Writing {registrantInformation} to {databaseType}.{REGISTRANTS}...", newRegistrants, databaseType,
                        REGISTRANTS);

                    _database.Registrants.InsertAllOnSubmit(uniqueRegistrants);

                    _database.SubmitChanges();

                    _logger.Information($"Finished writing {registrantInformation} to {databaseType}.{REGISTRANTS}.", newRegistrants,
                        databaseType,
                        REGISTRANTS);
                }
                catch (Exception ex)
                {
                    _logger.ForContext<DCCKellyDatabase>()
                           .Error(ex, $"Failed to write {0} to {1}.{2}", newRegistrants, databaseType, REGISTRANTS);
                }
            }
            else
            {
                _logger.Information($"No new registrants to write to {databaseType}.{REGISTRANTS}", databaseType, REGISTRANTS);
            }
        }

        private void WriteTalkInterests(IEnumerable<RegistrantInformation> registrantInformation,
            string databaseType)
        {
            const string TALKINTERESTS = nameof(_database.TalkInterest);
            List<(string FirstName, string LastName, List<int> Interests)> interests =
                registrantInformation.Select(reg => (reg.FirstName, reg.LastName, reg.TalkInterests)).ToList();

            try
            {
                _logger.Information($"Writing Talk Interests {interests} to {databaseType}.{TALKINTERESTS}...", interests, databaseType,
                    TALKINTERESTS);

                var talkInterestList = MatchTalkInterestsToTalks(_database, interests);

                _database.TalkInterest.InsertAllOnSubmit(talkInterestList);

                _database.SubmitChanges();

                _logger.Information($"Finished writing {talkInterestList.Count} Talk Interest records to {databaseType}.{TALKINTERESTS}.",
                    talkInterestList.Count, databaseType,
                    TALKINTERESTS);
            }
            catch (Exception ex)
            {
                _logger.ForContext<DCCKellyDatabase>().Error(ex, $"Failed to write {0} to {1}.{2}", interests, databaseType, TALKINTERESTS);
            }
        }

        private static List<Registrant> MapRegistrantInformationToRegistrantTable(IEnumerable<RegistrantInformation> registrants)
        {
            return registrants.Select(regInfo => new Registrant
                              {
                                  FirstName = regInfo.FirstName,
                                  LastName = regInfo.LastName,
                                  EmailAddress = regInfo.EmailAddress,
                                  Occupation = regInfo.Occupation,
                                  BirthDate = regInfo.BirthDate,
                                  UpdateTime = DateTime.Now,
                                  DiagnosticInformation = new StackTrace().ToString()
                              })
                              .Distinct()
                              .ToList();
        }

        private static List<TalkInterest> MatchTalkInterestsToTalks(DCCKellyDatabase database,
            IEnumerable<(string FirstName, string LastName, List<int> Interests)> interests)
        {
            return (from record in interests
                    from talkID in record.Interests
                    // Make sure we don't try to add any talk interests that don't have an existing talk id
                    let talkExists = database.Talks.Any(talk => talk.ID == talkID)
                    let registrantID = database.Registrants.Single(reg =>
                        reg.FirstName == record.FirstName &&
                        reg.LastName == record.LastName).ID
                    where talkExists
                    select new TalkInterest
                    {
                        InterestedRegistrantID = registrantID,
                        TalkID = talkID,
                        UpdateTime = DateTime.Now,
                        DiagnosticInformation = new StackTrace().ToString()
                    }).ToList();
        }
    }
}