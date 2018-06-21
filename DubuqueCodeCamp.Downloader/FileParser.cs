using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace DubuqueCodeCamp.Downloader
{
    /// <summary>
    /// Class to handle parsing a locally saved file
    /// </summary>
    public class FileParser
    {
        private const string DELIMITER = "|";

        /// <summary>
        /// Parse the given file
        /// </summary>
        /// <param name="filePath">Path to the file, including the name of the file to parse</param>
        /// <returns>List of <see cref="RegistrantInformation"/></returns>
        public static IEnumerable<RegistrantInformation> ParseFile(string filePath)
        {
            var csvreader = GetCsvReader(filePath);

            Console.WriteLine("\nReading file...");

            var records = csvreader.GetRecords<RegistrantInformation>().ToList();

            Console.WriteLine("\nFinished reading file " + filePath);

            return records;
        }

        private static CsvReader GetCsvReader(string filePath)
        {
            var textReader = new StreamReader(filePath);
            var csvreader = new CsvReader(textReader);

            // Configure the CsvReader
            csvreader.Configuration.Delimiter = DELIMITER;
            csvreader.Configuration.HasHeaderRecord = false;
            csvreader.Configuration.DetectColumnCountChanges = true;
            csvreader.Configuration.IgnoreBlankLines = true;
            csvreader.Configuration.TrimOptions = TrimOptions.Trim;
            csvreader.Configuration.AllowComments = true;
            csvreader.Configuration.DetectColumnCountChanges = false;
            return csvreader;
        }
    }
}
