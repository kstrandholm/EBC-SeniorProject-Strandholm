using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Class that represents a record in the Rooms table
    /// </summary>
    [Table(Name = "Rooms")]
    public class Room
    {
        /// <summary>
        /// ID of the Room record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <summary>
        /// Name of the room represented by this record
        /// </summary>
        [Column]
        public string RoomName { get; set; }

        /// <summary>
        /// Capacity of the room represented by this record
        /// </summary>
        [Column]
        public int Capacity { get; set; }
        
        /// <summary>
        /// Venue location of the room represented by this record
        /// </summary>
        [Column]
        public string Venue { get; set; }

        /// <summary>
        /// Last update time of this Room record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this Room record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
