using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace DubuqueCodeCamp.Downloader
{
    public class FileParser
    {
        // TODO: Take the CsvReader out of here so I don't have to setup the configuration everytime
        private const string DELIMITER = "|";

        public static IEnumerable<RegistrantInformation> ParseFile(string filePath)
        {
            Console.WriteLine("\nReading file...");

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

            var records = csvreader.GetRecords<RegistrantInformation>().ToList();

            Console.WriteLine("\nFinished reading file " + filePath);

            return records;
        }
    }
}
