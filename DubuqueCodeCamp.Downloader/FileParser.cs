using CsvHelper;
using CsvHelper.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DubuqueCodeCamp.Downloader
{
    /// <summary>
    /// Class to handle parsing a locally saved file
    /// </summary>
    public class FileParser
    {
        private const string DELIMITER = "|";
        private readonly ILogger _logger;
        private readonly TextReader _fileReader;

        /// <summary>
        /// Constructor for the File Parser
        /// </summary>
        /// <param name="fileReader"><see cref="TextReader"/> of the file to parse</param>
        public FileParser(TextReader fileReader)
        {
            _logger = LoggingInitializer.GetLogger();
            _fileReader = fileReader;
        }

        /// <summary>
        /// Parse the given file
        /// </summary>
        /// <returns>List of <see cref="RegistrantInformation"/></returns>
        public List<RegistrantInformation> ParseFile()
        {
            var csvreader = GetCsvReader(_fileReader);
            List<RegistrantInformation> records;

            _logger.Information("\nReading file...");
            try
            {
                records = csvreader.GetRecords<RegistrantInformation>().ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to parse the file", ex);
            }
            _logger.Information("\nFinished reading the file.");

            return records;
        }

        private CsvReader GetCsvReader(TextReader file)
        {
            var csvreader = new CsvReader(file);

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
