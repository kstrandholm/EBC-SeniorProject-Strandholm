using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <inheritdoc />
    /// <summary>
    /// Class to connect to the database and easily access the tables
    /// </summary>
    [Database(Name = "DCC_Kelly")]
    public class DCCKellyDatabase : DataContext
    {
        /// <inheritdoc />
        /// <summary>
        /// Database constructor
        /// </summary>
        public DCCKellyDatabase() : base(ConfigurationManager.ConnectionStrings["DCCDatabase"].ConnectionString) { }

        /// <summary>
        /// Linq representation of the database table Registrants
        /// </summary>
        public Table<Registrant> Registrants;

        /// <summary>
        /// Linq representation of the database table Rooms
        /// </summary>
        public Table<Room> Rooms;

        /// <summary>
        /// Linq representation of the database table Sessions
        /// </summary>
        public Table<Session> Sessions;

        /// <summary>
        /// Linq representation of the database table TalkInteres
        /// </summary>
        public Table<TalkInterest> TalkInterest;

        /// <summary>
        /// Linq representation of the database table Talks
        /// </summary>
        public Table<Talk> Talks;

        /// <summary>
        /// Linq representation of the database table Speakers
        /// </summary>
        public Table<Speaker> Speakers;

        /// <summary>
        /// Linq representation of the database table ProposedSchedules
        /// </summary>
        public Table<ProposedSchedule> ProposedSchedules;
    }
}
