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

            try
            {
                // Download the file from the FTP site
                DownloadFile(fileName, localFileLocation);

                // Parse the file from the local file path
                var registrants = GetParsedFileRecords(localFileLocation, fileName);

                // Write the records to the database
                WriteRecords(registrants);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "\nOh no, something went wrong!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
        }

        private static void DownloadFile(string fileName, string localFileLocation)
        {
            Console.WriteLine("Getting File " + fileName + "...\n");

            var fileDownloaded = SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

            // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
            var messageToUse = fileDownloaded ? "\nFile retrieved." : "\nFile already exists and does not need to be re-downloaded.";
            Console.WriteLine(messageToUse);
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

        private static void WriteRecords(IEnumerable<RegistrantInformation> registrantInformation)
        {
            Console.WriteLine("Writing records to the table...");

            var database = new DCCKellyDatabase();
            //var myregistrants = database.Registrant;
            //var mystuff = myregistrants.Where(r => r.LastName == "Strandholm");

            var registrantTable = MapRegistrantInformationToRegistrantTable(registrantInformation);

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
