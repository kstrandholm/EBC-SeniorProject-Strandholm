using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Table]
    public class ProposedSchedule
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public int SessionID { get; set; }

        [Column]
        public int RoomID { get; set; }

        [Column]
        public int TalkID { get; set; }

        [Column]
        public DateTime UpdateTime { get; set; }

        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
