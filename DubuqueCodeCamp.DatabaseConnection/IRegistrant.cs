using System;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <inheritdoc />
    /// <summary>
    /// Interface that represents the minimum fields required for a Registrant class
    /// </summary>
    public interface IRegistrant : IPerson
    {
        /// <summary>
        /// Occupation of the registrant
        /// </summary>
        string Occupation { get; set; }

        /// <summary>
        /// BirthDate of the registrant
        /// </summary>
        DateTime? BirthDate { get; set; }
    }
}
