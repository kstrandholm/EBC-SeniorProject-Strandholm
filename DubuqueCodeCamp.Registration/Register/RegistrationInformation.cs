using DubuqueCodeCamp.DatabaseConnection;
using System;
using System.Collections.Generic;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// Class that represents the information indicated during on-site registration
    /// </summary>
    public class RegistrationInformation : IRegistrant
    {
        /// <inheritdoc />
        /// <summary>
        /// First name of the person registering. Required field
        /// </summary>
        public string FirstName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Last name of the person registering. Required field
        /// </summary>
        public string LastName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Email address of the person registering. Required field
        /// </summary>
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Occupation of the person registering. Optional field
        /// </summary>
        public string Occupation { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Birth date of the person registering. Optional field
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// List of talks indicating which ones the person registering is interested in attending
        /// </summary>
        public List<ChosenTalk> ChosenTalks { get; set; } = DatabaseOperations.GetChosenTalks();
    }
}
