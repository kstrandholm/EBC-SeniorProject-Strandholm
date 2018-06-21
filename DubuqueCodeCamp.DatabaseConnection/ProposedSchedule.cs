using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Class that represents a Schedule entry in the ProposedSchedules table
    /// </summary>
    [Table]
    public class ProposedSchedule
    {
        /// <summary>
        /// ID of the Schedule entry
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <summary>
        /// ID of the Session related to this Schedule entry
        /// </summary>
        [Column]
        public int SessionID { get; set; }

        /// <summary>
        /// ID of the Room related to this Schedule entry
        /// </summary>
        [Column]
        public int RoomID { get; set; }

        /// <summary>
        /// ID of the Talk related to this Schedule entry
        /// </summary>
        [Column]
        public int TalkID { get; set; }

        /// <summary>
        /// Last updated time of this Schedule entry
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Lastest diagnostic information on this Schedule entry
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
