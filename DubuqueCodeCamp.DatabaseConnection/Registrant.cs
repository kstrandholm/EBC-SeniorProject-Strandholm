﻿using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <inheritdoc cref="IRegistrant" />
    /// <summary>
    /// Class that represents a record in the Registrants table
    /// </summary>
    [Table(Name = "Registrants")]
    public class Registrant : IRegistrant, IEquatable<IRegistrant>, IEquatable<Registrant>
    {
        /// <summary>
        /// ID of the Registrant record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// First Name of the person represented by this Registrant record
        /// </summary>
        [Column]
        public string FirstName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Last Name of the person represented by this Registrant record
        /// </summary>
        [Column]
        public string LastName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Email Address of the person represented by this Registrant record
        /// </summary>
        [Column]
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Occupation of the person represented by this Registrant record
        /// </summary>
        [Column(CanBeNull = true)]
        public string Occupation { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// BirthDate of the person represented by this Registrant record
        /// </summary>
        [Column(CanBeNull = true)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Last update time of this Registrant record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this Registrant record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }

        /// <inheritdoc />
        public bool Equals(Registrant other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) &&
                   string.Equals(EmailAddress, other.EmailAddress);
        }

        /// <inheritdoc />
        public bool Equals(IRegistrant other)
        {
            if (other is null)
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
            if (obj.GetType() != GetType())
                return false;

            return Equals((IRegistrant) obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="left">First registrant to compare</param>
        /// <param name="right">Second registrant to compare</param>
        /// <returns>
        /// <see langword="true" /> if the left registrant is equal to the right registrant; otherwise, <see langword="false" />.
        /// </returns>
        public static bool Equals(Registrant left, Registrant right)
        {
            if (left is null || right is null)
                return false;
            if (ReferenceEquals(left, right))
                return true;
            if (left.GetType() != right.GetType())
                return false;

            return string.Equals(left.FirstName, right.FirstName) && string.Equals(left.LastName, right.LastName) &&
                   string.Equals(left.EmailAddress, right.EmailAddress);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FirstName.GetHashCode();
                hashCode = (hashCode * 397) ^ LastName.GetHashCode();
                hashCode = (hashCode * 397) ^ EmailAddress.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:DubuqueCodeCamp.DatabaseConnection.Registrant" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Registrant left, Registrant right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:DubuqueCodeCamp.DatabaseConnection.Registrant" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Registrant left, Registrant right)
        {
            return !Equals(left, right);
        }
    }
}
