using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubuqueCodeCampScheduler
{
    public class TalkInterest
    {
        public Guid ID { get; private set; }

        public Guid TalkID { get; private set; }

        public Guid InterestedRegistrantID { get; private set; }
    }
}
