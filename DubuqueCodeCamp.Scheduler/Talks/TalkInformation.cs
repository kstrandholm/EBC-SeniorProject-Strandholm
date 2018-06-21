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
        /// First name of the seaker of this talk
        /// </summary>
        public string SpeakerFirstName { get; set; }

        /// <summary>
        /// Last name of the seaker of this talk
        /// </summary>
        public string SpeakerLastName { get; set; }
    }
}