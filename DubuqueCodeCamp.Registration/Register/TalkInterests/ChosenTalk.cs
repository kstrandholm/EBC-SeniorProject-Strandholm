namespace DubuqueCodeCamp.Registration
{
    /// <summary>
    /// Information on a Talk and who will be giving it, as well as an option to chose that talk
    /// </summary>
    public class ChosenTalk
    {
        /// <summary>
        /// The database ID for the talk
        /// </summary>
        public int TalkID { get; set; }

        /// <summary>
        /// Title of the talk
        /// </summary>
        public string TalkTitle { get; set; }

        /// <summary>
        /// Summary of the talk
        /// </summary>
        public string TalkSummary { get; set; }

        /// <summary>
        /// First name of this talk's speaker
        /// </summary>
        public string SpeakerFirstName { get; set; }

        /// <summary>
        /// Last name of this talk's speaker
        /// </summary>
        public string SpeakerLastName { get; set; }

        /// <summary>
        /// Whether this talk has been selected as an interest by the currently registering person
        /// </summary>
        public bool Chosen { get; set; }
    }
}
