using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Class that represents a record in the Talks table
    /// </summary>
    [Table(Name = "Talks")]
    public class Talk
    {
        /// <summary>
        /// ID of the Speaker record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <summary>
        /// Title of the talk represented by this record
        /// </summary>
        [Column]
        public string Title { get; set; }


        /// <summary>
        /// Summary of the talk represented by this record
        /// </summary>
        [Column]
        public string Summary { get; set; }

        /// <summary>
        /// ID of the Speaker related to this Talk record
        /// </summary>
        [Column]
        public int SpeakerID { get; set; }

        /// <summary>
        /// Date this talk was or will be given
        /// </summary>
        [Column(CanBeNull = true)]
        // TODO: make non-nullable after dealing with database null nonsense
        public DateTime? DateGiven { get; set; }

        /// <summary>
        /// Last update time of this Talk record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this Talk record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
