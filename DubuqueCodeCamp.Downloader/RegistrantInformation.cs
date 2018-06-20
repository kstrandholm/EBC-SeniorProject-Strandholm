using System;
using System.Collections.Generic;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Downloader
{
    public class RegistrantInformation : IRegistrant, IEquatable<RegistrantInformation>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string EmailAddress { get; set; }

        public List<int> TalkInterests { get; set; }

        public string Occupation { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool Equals(RegistrantInformation other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(City, other.City) && string.Equals(State, other.State);
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
                hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
