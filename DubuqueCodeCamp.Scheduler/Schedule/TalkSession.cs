using System;
using System.Collections.Generic;
using System.Linq;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    /// <summary>
    /// Class that represents all the data contained in the Proposed Schedule ListView
    /// </summary>
    public class TalkSession
    {
        public DateTime SessionDate { get; set; }

        public DateTime SessionStartTime { get; set; }

        public DateTime SessionEndTime { get; set; }

        public int SessionLengthMinutes { get; set; }

        public string RoomName { get; set; }

        public int RoomCapacity { get; set; }

        public string Venue { get; set; }

        public string TalkTitle { get; set; }

        public string TalkSummary { get; set; }

        public string SpeakerFirstName { get; set; }

        public string SpeakerLastName { get; set; }
    }
}
