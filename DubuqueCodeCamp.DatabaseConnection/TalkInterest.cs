using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Class that represents a record in the TalkInterests table
    /// </summary>
    [Table]
    public class TalkInterest
    {
        /// <summary>
        /// ID of the TalkInterest record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <summary>
        /// ID of the Talk related to this TalkInterest record
        /// </summary>
        [Column]
        public int TalkID { get; set; }

        /// <summary>
        /// ID of the Registrant interested in the talk associated with this TalkInterest record
        /// </summary>
        [Column]
        public int InterestedRegistrantID { get; set; }

        /// <summary>
        /// Last update time of this TalkInterest record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this TalkInterest record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
