using System;

namespace DubuqueCodeCampScheduler
{
    public class Talks
    {
        public Guid ID { get; private set; }

        public string Title { get; private set; }

        public string Summary { get; private set; }

        public string DateGiven { get; private set; }
    }
}
