using DubuqueCodeCamp.DatabaseConnection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace DubuqueCodeCamp.Downloader
{
    public class Runner
    {
        public static void Main(string[] args)
        {
            var localFileLocation = ConfigurationManager.AppSettings["LocalFileLocation"];
            var fileName = "SampleFile.txt";
            var logger = LoggingInitializer.InitializeLogger();

            try
            {
                // Download the file from the FTP site
                DownloadFile(fileName, localFileLocation, logger);

                // Parse the file from the local file path
                var registrants = GetParsedFileRecords(localFileLocation, fileName);

                using (var database = new DCCKellyDatabase())
                {
                    // Write the records to the database
                    WriteRecords(database, registrants, logger);
                }
            }
            catch (Exception ex)
            {
                logger.ForContext<Runner>().Error(ex, "\nOh no, something went wrong!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
            }

#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
        }

        private static void DownloadFile(string fileName, string localFileLocation, ILogger logger)
        {
            logger.Information("Getting File " + fileName + "...\n");

            var fileDownloaded = SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

            // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
            var messageToUse = fileDownloaded ? "\nFile retrieved." : "\nFile already exists and does not need to be re-downloaded.";
            logger.Information(messageToUse);
        }

        private static IEnumerable<RegistrantInformation> GetParsedFileRecords(string localFileLocation, string fileName)
        {
            var filePath = localFileLocation + fileName;
            if (File.Exists(filePath))
                return FileParser.ParseFile(filePath).ToList();
            else
            {
                throw new FileNotFoundException($"File {fileName} does not exist in {localFileLocation}.", fileName);
            }
        }

        private static void WriteRecords(DCCKellyDatabase database, IEnumerable<RegistrantInformation> registrantInformation,
            ILogger logger)
        {
            logger.Information("Writing records to the table...");
            //var myregistrants = database.Registrant;
            //var mystuff = myregistrants.Where(r => r.LastName == "Strandholm");

            var registrantTable = MapRegistrantInformationToRegistrantTable(registrantInformation);

            // TODO: Implement logic to prevent existing people from being added
            // TODO: Requires equality checks
            database.Registrants.InsertAllOnSubmit(registrantTable);

            database.SubmitChanges();
        }

        private static IEnumerable<Registrant> MapRegistrantInformationToRegistrantTable(IEnumerable<RegistrantInformation> registrants)
        {
            var registrantsTable = new List<Registrant>();
            foreach (var regInfo in registrants)
            {
                var reg = new Registrant()
                {
                    FirstName = regInfo.FirstName,
                    LastName = regInfo.LastName,
                    StreetAddress = regInfo.StreetAddress,
                    City = regInfo.City,
                    State = regInfo.State,
                    EmailAddress = regInfo.EmailAddress,
                    IsSpeaker = regInfo.IsSpeaker
                };
                registrantsTable.Add(reg);
            }

            return registrantsTable;
        }
    }
}
