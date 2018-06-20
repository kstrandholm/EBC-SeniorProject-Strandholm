using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Registration
{
    public class DatabaseOperations
    {
        private static DCCKellyDatabase _database = new DCCKellyDatabase();

        public static void GetTalks()
        {
            var talks = _database.Talks.Where(talk => talk.ID == 7);
        }

        public static void SaveRegistration()
        {
            _database.SubmitChanges();
        }
    }
}
