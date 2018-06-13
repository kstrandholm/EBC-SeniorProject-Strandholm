using System.Data.Linq.Mapping;
using System.Runtime.InteropServices;

namespace DubuqueCodeCamp.Downloader
{
    [Table(Name = "Registrants")]
    public class RegistrantInformation
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column(CanBeNull = true)]
        public string StreetAddress { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string State { get; set; }

        [Column(CanBeNull = true)]
        public string EmailAddress { get; set; }

        [Column]
        public bool IsSpeaker { get; set; }

    }
}
