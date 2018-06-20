using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Registration
{
    public class RegistrationInformation : IRegistrant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Occupation { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
