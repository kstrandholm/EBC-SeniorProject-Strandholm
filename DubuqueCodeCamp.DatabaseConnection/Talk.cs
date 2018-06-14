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
        public int SpeakerID { get; set; }
    }
}
