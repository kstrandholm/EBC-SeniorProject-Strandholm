using System;

namespace DubuqueCodeCampScheduler
{
    public class Registrant
    {
        public int ID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string StreetAddress { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string EmailAddress { get; private set; }

        public bool IsSpeaker { get; private set; }
    }
}
