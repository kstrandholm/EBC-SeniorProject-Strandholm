using System;

namespace DubuqueCodeCampScheduler
{
    public class Talks
    {
        public Guid ID { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        // Ignoring this for now while I get the program set up to hanlde just one event
        //public string DateGiven { get; private set; }
    }
}
