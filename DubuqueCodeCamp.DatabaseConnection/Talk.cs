using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Table(Name = "Talks")]
    public class Talk
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Summary { get; set; }

        [Column]
        // TODO: determine if want to include Speaker class to make explicit relationship here
        public int SpeakerID { get; set; }

        [Column(CanBeNull = true)]
        // TODO: make non-nullable after dealing with database null nonsense
        public DateTime? DateGiven { get; set; }

        [Column]
        public DateTime UpdateTime { get; set; }

        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
