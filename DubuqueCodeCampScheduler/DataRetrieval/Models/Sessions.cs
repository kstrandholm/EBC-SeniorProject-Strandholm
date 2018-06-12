using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
