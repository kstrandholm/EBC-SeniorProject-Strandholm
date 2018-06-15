using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Block of time available for talks during the code camp event
    /// </summary>
    /// <remarks>It is not the concern of this project to ensure there are sufficient breaks</remarks>
    /// <example>9AM to 10:15AM on July 11th, 2018</example>
    [Table(Name = "Sessions")]
    public class Session
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public DateTime TimeStart { get; set; }

        [Column]
        public DateTime TimeEnd { get; set; }

        [Column(IsDbGenerated = true)]
        public int CalcLengthMinutes { get; set; }

        [Column]
        public DateTime UpdateTime { get; set; }

        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
