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
                logger.ForContext<FileParser>().Fatal(ex, $"Failed to parse {fileName} at '{localFileLocation}'.", fileName, localFileLocation);
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

        private static void WriteRecords(DCCKellyDatabase database, IEnumerable<RegistrantInformation> registrantInformation,
            ILogger logger)
        {
            const string DATABASE = nameof(database);
            const string TABLE = nameof(database.Registrants);

            logger.Information($"Writing {registrantInformation} to {DATABASE}.{TABLE}...", nameof(registrantInformation), DATABASE, TABLE);
            var databaseRegistrants = database.Registrants;

            // Convert the RegistrantInformation from the parser into a format that can be saved to the database
            var registrantTable = MapRegistrantInformationToRegistrantTable(registrantInformation);

            var uniqueRegistrants = new List<Registrant>();
            foreach (var databaseReg in databaseRegistrants)
            {
                // TODO: Implement equality checks elsewhere for reuse
                uniqueRegistrants.AddRange(registrantTable.Where(newReg =>
                    !(newReg.FirstName == databaseReg.FirstName && newReg.LastName == databaseReg.LastName &&
                      newReg.City == databaseReg.City && newReg.State == databaseReg.State)));
            }

            try
            {
                database.Registrants.InsertAllOnSubmit(uniqueRegistrants);

                database.SubmitChanges();
            }
            catch (Exception ex)
            {
                logger.ForContext<DCCKellyDatabase>().Error(ex, $"Writing {0} to {1}.{2} failed.", registrantTable, DATABASE, TABLE);
            }
        }

        private static List<Registrant> MapRegistrantInformationToRegistrantTable(IEnumerable<RegistrantInformation> registrants)
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
