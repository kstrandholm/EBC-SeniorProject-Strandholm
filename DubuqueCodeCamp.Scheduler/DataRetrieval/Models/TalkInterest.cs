using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuqueCodeCampScheduler
{
    public class TalkInterest
    {
        public int ID { get; private set; }

        public Talk Talk { get; private set; }

        public Registrant InterestedRegistrant { get; private set; }
    }
}
