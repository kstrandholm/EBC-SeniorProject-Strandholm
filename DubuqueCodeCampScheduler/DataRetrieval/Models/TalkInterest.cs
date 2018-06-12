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

        public Talk TalkID { get; private set; }

        public Registrant InterestedRegistrantID { get; private set; }
    }
}
