using System;
using System.Collections.Generic;
using System.Linq;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    public class TalkSession
    {
        private readonly DCCKellyDatabase _database = new DCCKellyDatabase();

        public Session Session { get; set; }

        public Room Room { get; set; }

        public Talk Talk { get; set; }
    }
}
