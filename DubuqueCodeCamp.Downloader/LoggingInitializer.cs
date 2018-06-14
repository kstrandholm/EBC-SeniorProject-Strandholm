using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

namespace DubuqueCodeCamp.Downloader
{
    public class LoggingInitializer
    {
        public static ILogger InitializeLogger()
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
                    new DataColumn {DataType = typeof(string), ColumnName = "StackTrace", AllowDBNull = false},
                    new DataColumn {DataType = typeof(CallSite), ColumnName = "CallSite", AllowDBNull = false},
                    new DataColumn {DataType = typeof(string), ColumnName = "Logger", AllowDBNull = false}
                },
                Id = { ColumnName = "ID" },
                TimeStamp = { ColumnName = "Date" }
            };
            
            return new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(databaseConnectionString, "Log", LogEventLevel.Information, columnOptions: columnOptions)
                .WriteTo.RollingFile(rollingFileLocation)
                .CreateLogger();
        }
    }
}
