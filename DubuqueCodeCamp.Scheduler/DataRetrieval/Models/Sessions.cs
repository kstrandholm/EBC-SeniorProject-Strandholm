using System;

namespace DubuqueCodeCampScheduler
{
    public class Sessions
    {
        public int ID { get; private set; }

        public DateTime SessionStart { get; private set; }

        public DateTime SessionEnd { get; private set; }

       public int LengthInMinutes { get; private set; }
    }
}
