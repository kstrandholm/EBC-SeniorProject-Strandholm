using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Table]
    public class Talk
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; }

        [Column]
        public string Title { get; }

        [Column]
        public string Summary { get; }

        [Column]
        public Registrant Presenter { get; }
    }
}
