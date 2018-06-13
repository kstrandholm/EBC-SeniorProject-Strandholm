using System;
using System.Collections.Generic;
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

        public DCCKellyDatabase() : base("Server=tcp:pltnm-dev-testing.database.windows.net,1433;Initial Catalog=DCC_Kelly;Persist Security Info=False;User ID=kstrandholm;Password=g83y9hAHiTyc38NjJUF8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;") { }

        public Table<RegistrantInformation> Registrants;

        //public Table<Room> Rooms;

        //public Table<Session> Sessions;

        //public Table<TalkInterest> TalkInterests;

        //public Table<Talk> Talks;

    }
}
