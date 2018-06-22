using System;
using System.Collections.Generic;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Downloader
{
    /// <inheritdoc cref="IRegistrant" />
    /// <summary>
    /// Class that represents a single record in the Registrant Download File
    /// </summary>
    public class RegistrantInformation : IRegistrant, IEquatable<RegistrantInformation>
    {
        /// <inheritdoc />
        /// <summary>
        /// First Name of the registrant
        /// </summary>
        public string FirstName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Last Name of the registrant
        /// </summary>
        public string LastName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Email Address of the registrant
        /// </summary>
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Occupation of the registrant
        /// </summary>
        public string Occupation { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Birth date of the registrant
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// List of talks the registrant is interested in attending
        /// </summary>
        public List<int> TalkInterests { get; set; }

        /// <inheritdoc />
        public bool Equals(RegistrantInformation other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) &&
                   string.Equals(EmailAddress, other.EmailAddress);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;

            return Equals((RegistrantInformation)obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
