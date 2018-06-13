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

            // Download the file from the FTP site
            DownloadFile(fileName, localFileLocation);

            // Parse the file from the local file path
            var registrants = GetParsedFileRecords(localFileLocation, fileName);

            // Write the records to the database
            WriteRecords(registrants);


#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
        }

        private static void DownloadFile(string fileName, string localFileLocation)
        {
            try
            {
                Console.WriteLine("Getting File " + fileName + "...\n");

                var fileDownloaded = SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                var messageToUse = fileDownloaded ? "\nFile retrieved." : "\nFile already exists and does not need to be re-downloaded.";
                Console.WriteLine(messageToUse);
            }
            catch (Exception ex)
            {
                // Something went wrong - indicate so
                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                Console.WriteLine(
                    "\nOh no, something went wrong with the file download!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private static IEnumerable<RegistrantInformation> GetParsedFileRecords(string localFileLocation, string fileName)
        {
            var filePath = localFileLocation + fileName;
            try
            {
                if (File.Exists(filePath))
                    return FileParser.ParseFile(filePath).ToList();
                else
                    throw new FileNotFoundException($"File {fileName} does not exist in {localFileLocation}.", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "\nOh no, something went wrong with parsing the file!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                return new List<RegistrantInformation>();
            }
        }

        private static void WriteRecords(IEnumerable<RegistrantInformation> registrants)
        {
            Console.WriteLine();
            foreach (var registrant in registrants)
            {
                Console.WriteLine(registrant.FirstName + " " + registrant.LastName + " " + registrant.IsSpeaker);
            }

            try { }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
