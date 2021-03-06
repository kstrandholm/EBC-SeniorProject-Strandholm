﻿using DubuqueCodeCamp.DatabaseConnection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace DubuqueCodeCamp.Downloader
{
    /// <summary>
    /// Class that runs the File Downloader to download and save the registrant information file from the 3rd party
    /// </summary>
    public class Runner
    {
        /// <summary>
        /// Main method that handles the organization of downloading and saving the registrant information file from the 3rd party
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
#if DEBUG
            Serilog.Debugging.SelfLog.Enable(Console.Error);
#endif
            var localFileLocation = ConfigurationManager.AppSettings["LocalFileLocation"];
            var fileName = "SampleFile.txt";

            using (var logger = LoggingInitializer.GetLogger())
            {
                // Download the file from the FTP site
                var ftp = new SftpDownload();
                var messageToUse = ftp.DownloadFileUsingSftpClient(fileName, localFileLocation)
                    ? "\nFile retrieved."
                    : "\nFile not downloaded.";
                logger.Information(messageToUse, fileName, localFileLocation);

                // Parse the file from the local file path
                List<RegistrantInformation> registrants;
                var filePath = localFileLocation + fileName;

                if (File.Exists(filePath))
                {
                    var streamReader = new StreamReader(filePath);
                    var fileParser = new FileParser(streamReader);

                    registrants = fileParser.ParseFile();
                }
                else
                {
                    // If the file does not exist, throw an exception
                    throw new FileNotFoundException($"File {fileName} does not exist in {localFileLocation}.", fileName);
                }

                // If the parser could not get any records, don't try to save to the database
                if (registrants.Any())
                {
                    // Write the records to the database
                    var writer = new WriteToDatabase();
                    writer.WriteDownloadRecords(registrants);
                }

#if DEBUG
                Console.WriteLine("\nPress any key to Continue...");
                Console.ReadKey();
#endif
            }
        }
    }
}
