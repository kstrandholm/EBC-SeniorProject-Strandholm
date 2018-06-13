using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuqueCodeCamp.Downloader
{
    [Database(Name = "DCC_Kelly")]
    public class DCCKellyDatabase : DataContext
    {

        public DCCKellyDatabase() : base(ConfigurationManager.ConnectionStrings["DCCDatabase"].ConnectionString) { }

        public Table<RegistrantInformation> Registrants;

        //public Table<Room> Rooms;

        //public Table<Session> Sessions;

        //public Table<TalkInterest> TalkInterests;

        //public Table<Talk> Talks;

    }
}
