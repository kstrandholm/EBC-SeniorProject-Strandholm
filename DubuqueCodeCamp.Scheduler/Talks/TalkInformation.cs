using System;

namespace DubuqueCodeCamp.Scheduler
{
    /// <summary>
    /// Class that represents the information related to a Talk that is displayed in the <seealso cref="TalksView"/>
    /// </summary>
    public class TalkInformation
    {
        /// <summary>
        /// Date the talk was or will be given
        /// </summary>
        public DateTime TalkDate { get; set; }

        /// <summary>
        /// Title of the talk
        /// </summary>
        public string TalkTitle { get; set; }

        /// <summary>
        /// Summary of the talk
        /// </summary>
        public string TalkSummary { get; set; }

        /// <summary>
        /// First and last name of the seaker of this talk
        /// </summary>
        public string SpeakerName { get; set; }
    }
}