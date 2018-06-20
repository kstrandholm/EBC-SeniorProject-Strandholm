using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using Serilog.Core;

namespace DubuqueCodeCamp.Downloader
{
    public class LoggingInitializer
    {
        public static Logger GetLogger()
        {
            var databaseConnectionString = ConfigurationManager.ConnectionStrings["DCCDatabase"].ConnectionString;
            var rollingFileLocation = ConfigurationManager.AppSettings["LoggingFileLocation"];

            var columnOptions = new ColumnOptions
            {
                Store = new Collection<StandardColumn>
                {
                    StandardColumn.TimeStamp,   // Date
                    StandardColumn.Level,
                    StandardColumn.Message,
                    StandardColumn.Exception,
                },
                AdditionalDataColumns = new Collection<DataColumn>
                {
                    new DataColumn {DataType = typeof(string), ColumnName = "Table", AllowDBNull = true},
                    new DataColumn {DataType = typeof(string), ColumnName = "MachineName", AllowDBNull = true},
                    new DataColumn {DataType = typeof(string), ColumnName = "SourceContext", AllowDBNull = true}
                },
                Id = { ColumnName = "ID" },
                TimeStamp = { ColumnName = "Date" }
            };
            
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.MSSqlServer(databaseConnectionString, "Log", LogEventLevel.Information, columnOptions: columnOptions)
                .WriteTo.Console()
                .WriteTo.RollingFile(rollingFileLocation)
                .CreateLogger();
        }
    }
}
