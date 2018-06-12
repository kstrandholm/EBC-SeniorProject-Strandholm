using System;

namespace DubuqueCodeCampScheduler
{
    public class ProposedSchedule
    {
        public int ID { get; private set; }

        public Room Room { get; private set; }

        public Talk Talk { get; private set; }

        // TODO: Figure out a way to determine how long the sessions are, when lunch would need to be, etc.
        public TimeSpan SessionTime { get; private set; }
    }
}
