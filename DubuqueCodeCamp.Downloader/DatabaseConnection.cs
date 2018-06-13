using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuqueCodeCamp.Downloader
{
    public class DatabaseConnection
    {
        private DataContext _context;

        public DatabaseConnection(string connection)
        {
            _context = new DataContext(connection);
        }

        public void WriteRegistrantToDatabase()
        {
            var Registrants = _context.GetTable<RegistrantInformation>();
            var newRegistrants = new RegistrantInformation()
            {
                FirstName = "Kelly",
                LastName = "Strandholm",
                City = "Dubuque",
                IsSpeaker = false
            };
            Registrants.InsertOnSubmit(newRegistrants);
            _context.SubmitChanges();
        }

    }
}
