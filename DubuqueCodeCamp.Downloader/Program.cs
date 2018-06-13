using System;
using System.Collections.Generic;
using System.Configuration;

namespace DubuqueCodeCamp.Downloader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var registrants = new List<RegistrantInformation>();

            var localFileLocation = ConfigurationManager.AppSettings["LocalFileLocation"];
            var fileName = "SampleFile.txt";

            try
            {
                Console.WriteLine("Getting File " + fileName + "...\n");

                // Download the file
                SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

                // If it suceeded, write so to the console
                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                Console.WriteLine("\nFile retrieved.");
            }
            catch (Exception ex)
            {
                // Something went wrong - indicate so
                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                Console.WriteLine(
                    "\nOh no, something went wrong with the file download!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            try
            {
                registrants = FileParser.ParseFile(localFileLocation + fileName).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "\nOh no, something went wrong with parsing the file!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
        }
    }
}
