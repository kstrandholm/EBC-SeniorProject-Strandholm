using System;
using DubuqueCodeCamp.DatabaseConnection;
using System.Linq;

namespace DubuqueCodeCamp.Registration
{
    public class DatabaseOperations
    {
        private static DCCKellyDatabase _database = new DCCKellyDatabase();

        public static void GetTalks()
        {
            // TODO: remove null check once database nullability stuff is taken care of
            var talks = _database.Talks.Where(talk => talk.DateGiven == DateTime.Today || talk.DateGiven == null);
        }

        public static void SaveRegistration()
        {
            _database.SubmitChanges();
        }
    }
}
