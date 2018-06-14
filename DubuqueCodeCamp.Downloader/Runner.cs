using DubuqueCodeCamp.DatabaseConnection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DubuqueCodeCamp.Downloader
{
    public class Runner
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Serilog.Debugging.SelfLog.Enable(Console.Error);
#endif
            var localFileLocation = ConfigurationManager.AppSettings["LocalFileLocation"];
            var fileName = "SampleFile.txt";
            var logger = LoggingInitializer.GetLogger();

            try
            {
                // Download the file from the FTP site
                DownloadFile(fileName, localFileLocation, logger);
            }
            catch (Exception ex)
            {
                logger.ForContext<SFTPDownload>().Error(ex, "SFTP Download failed.");
            }

            var registrants = new List<RegistrantInformation>();
            try
            {
                // Parse the file from the local file path
                registrants = GetParsedFileRecords(localFileLocation, fileName);
            }
            catch (Exception ex)
            {
                logger.ForContext<FileParser>()
                      .Fatal(ex, $"Failed to parse {fileName} at '{localFileLocation}'.", fileName, localFileLocation);
            }

            // If the parser could not get any records, don't try to save to the database
            if (registrants.Any())
            {
                using (var database = new DCCKellyDatabase())
                {
                    // Write the records to the database
                    WriteRecords(database, registrants, logger);
                }
            }

#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
            logger.Dispose();
        }

        private static void DownloadFile(string fileName, string localFileLocation, ILogger logger)
        {
            logger.Information("Getting File {fileName}...\n", fileName);

            var fileDownloaded = SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

            // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
            var messageToUse = fileDownloaded ? "\nFile retrieved." : "\nFile already exists and does not need to be re-downloaded.";
            logger.Information(messageToUse);
        }

        private static List<RegistrantInformation> GetParsedFileRecords(string localFileLocation, string fileName)
        {
            var filePath = localFileLocation + fileName;
            if (File.Exists(filePath))
                return FileParser.ParseFile(filePath).ToList();
            else
            {
                throw new FileNotFoundException($"File {fileName} does not exist in {localFileLocation}.", fileName);
            }
        }

        private static void WriteRecords(DCCKellyDatabase database, IReadOnlyCollection<RegistrantInformation> registrantInformation,
            ILogger logger)
        {
            const string DATABASE = nameof(database);
            const string TABLE = nameof(database.Registrants);

            var databaseRegistrants = database.Registrants;

            // Convert the RegistrantInformation from the parser into a format that can be saved to the database
            var newRegistrants = MapRegistrantInformationToRegistrantTable(registrantInformation);

            // TODO: Get Equality overrides to work
            // If the record is not a duplicate of what is already in the database, add it to the database
            var uniqueRegistrants = newRegistrants.Where(newReg => !databaseRegistrants.Any(dataReg =>
                dataReg.FirstName == newReg.FirstName && dataReg.LastName == newReg.LastName &&
                dataReg.City == newReg.City && dataReg.State == newReg.State)).ToList();

            // Try using the new Equals operator I implemented - can't figure out how to get this to work
            //var uniqueRegistrants = newRegistrants.Where(newReg => !databaseRegistrants.Any(dataReg =>
            //    dataReg == newReg)).ToList();

            if (uniqueRegistrants.Any())
            {
                // TODO: put this in a new class or method??
                try
                {
                    logger.Information($"Writing {registrantInformation} to {DATABASE}.{TABLE}...", newRegistrants, DATABASE, TABLE);

                    database.Registrants.InsertAllOnSubmit(uniqueRegistrants);

                    database.SubmitChanges();

                    logger.Information($"Finished writing {registrantInformation} to {DATABASE}.{TABLE}.", newRegistrants, DATABASE, TABLE);
                }
                catch (Exception ex)
                {
                    logger.ForContext<DCCKellyDatabase>().Error(ex, $"Writing {0} to {1}.{2} failed.", newRegistrants, DATABASE, TABLE);
                }
            }
        }

        private static List<Registrant> MapRegistrantInformationToRegistrantTable(IEnumerable<RegistrantInformation> registrants)
        {
            return registrants.Select(regInfo => new Registrant()
                              {
                                  FirstName = regInfo.FirstName,
                                  LastName = regInfo.LastName,
                                  StreetAddress = regInfo.StreetAddress,
                                  City = regInfo.City,
                                  State = regInfo.State,
                                  EmailAddress = regInfo.EmailAddress,
                                  IsSpeaker = regInfo.IsSpeaker,
                                  UpdateTime = DateTime.Now,
                                  DiagnosticInfo = new StackTrace().ToString()
                              })
                              .Distinct()
                              .ToList();
        }
    }
}
