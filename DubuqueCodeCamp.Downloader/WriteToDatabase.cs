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
        public static void WriteDownloadRecords(DCCKellyDatabase database, IReadOnlyCollection<RegistrantInformation> registrantInformation,
            ILogger logger)
        {
            var databaseType = database.GetType().ToString();

            WriteRegistrantInformation(database, registrantInformation, logger, databaseType);

            // Now map the registrant's Talk Interests
            WriteTalkInterests(database, registrantInformation, logger, databaseType);
        }

        private static void WriteRegistrantInformation(DCCKellyDatabase database,
            IReadOnlyCollection<RegistrantInformation> registrantInformation, ILogger logger,
            string databaseType)
        {
            const string REGISTRANTS = nameof(database.Registrants);

            var databaseRegistrants = database.Registrants;

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
                    logger.Information($"Writing {registrantInformation} to {databaseType}.{REGISTRANTS}...", newRegistrants, databaseType,
                        REGISTRANTS);

                    database.Registrants.InsertAllOnSubmit(uniqueRegistrants);

                    database.SubmitChanges();

                    logger.Information($"Finished writing {registrantInformation} to {databaseType}.{REGISTRANTS}.", newRegistrants,
                        databaseType,
                        REGISTRANTS);
                }
                catch (Exception ex)
                {
                    logger.ForContext<DCCKellyDatabase>()
                          .Error(ex, $"Failed to write {0} to {1}.{2}", newRegistrants, databaseType, REGISTRANTS);
                }
            }
            else
            {
                logger.Information($"No new registrants to write to {databaseType}.{REGISTRANTS}", databaseType, REGISTRANTS);
            }
        }

        private static void WriteTalkInterests(DCCKellyDatabase database, IEnumerable<RegistrantInformation> registrantInformation,
            ILogger logger,
            string databaseType)
        {
            const string TALKINTERESTS = nameof(database.TalkInterest);
            List<(string FirstName, string LastName, List<int> Interests)> interests =
                registrantInformation.Select(reg => (reg.FirstName, reg.LastName, reg.TalkInterests)).ToList();

            try
            {
                logger.Information($"Writing Talk Interests {interests} to {databaseType}.{TALKINTERESTS}...", interests, databaseType, TALKINTERESTS);

                var talkInterestList = MatchTalkInterestsToTalks(database, interests);

                database.TalkInterest.InsertAllOnSubmit(talkInterestList);

                database.SubmitChanges();

                logger.Information($"Finished writing {talkInterestList.Count} Talk Interest records to {databaseType}.{TALKINTERESTS}.", talkInterestList.Count, databaseType,
                    TALKINTERESTS);
            }
            catch (Exception ex)
            {
                logger.ForContext<DCCKellyDatabase>().Error(ex, $"Failed to write {0} to {1}.{2}", interests, databaseType, TALKINTERESTS);
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