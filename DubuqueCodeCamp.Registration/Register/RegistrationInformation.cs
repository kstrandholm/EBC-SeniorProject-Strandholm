using DubuqueCodeCamp.DatabaseConnection;
using System;
using System.Collections.Generic;

namespace DubuqueCodeCamp.Registration
{
    public class RegistrationInformation : IRegistrant
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Occupation { get; set; }

        public DateTime? BirthDate { get; set; }

        public List<ChosenTalk> ChosenTalks { get; set; } = DatabaseOperations.GetChosenTalks();
    }
}
