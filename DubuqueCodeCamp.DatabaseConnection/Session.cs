using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Class that represents a record in the Sessions table indicating a block of time available for talks
    /// </summary>
    /// <remarks>It is not the concern of this project to ensure there are sufficient breaks</remarks>
    /// <example>9AM to 10:15AM on July 11th, 2018</example>
    [Table(Name = "Sessions")]
    public class Session
    {
        /// <summary>
        /// ID of the Session record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <summary>
        /// Date the Session represented by this record occurs
        /// </summary>
        [Column]
        public DateTime SessionDate { get; set; }

        /// <summary>
        /// Time the Session represented by this record begins
        /// </summary>
        [Column]
        public DateTime TimeStart { get; set; }

        /// <summary>
        /// Time the Session represented by this record ends
        /// </summary>
        [Column]
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// Length of time in minutes that the Session represented by this record spans, calculated by the database
        /// </summary>
        [Column(IsDbGenerated = true)]
        public int CalcLengthMinutes { get; set; }

        /// <summary>
        /// Last update time of this Session record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this Session record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
