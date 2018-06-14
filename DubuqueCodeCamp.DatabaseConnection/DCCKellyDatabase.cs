using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Database(Name = "DCC_Kelly")]
    public class DCCKellyDatabase : DataContext
    {

        public DCCKellyDatabase() : base(ConfigurationManager.ConnectionStrings["DCCDatabase"].ConnectionString) { }

        public Table<Registrant> Registrants;

        //public Table<Room> Rooms;

        //public Table<Session> Sessions;

        public Table<TalkInterest> TalkInterest;

        public Table<Talk> Talks;

    }
}
