using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <inheritdoc />
    /// <summary>
    /// Class that represents a record in the Speakers table
    /// </summary>
    [Table(Name = "Speakers")]
    public class Speaker : IPerson
    {
        /// <summary>
        /// ID of the Speaker record
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// First Name of the person represented by this Speaker record
        /// </summary>
        [Column]
        public string FirstName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Last Name of the person represented by this Speaker record
        /// </summary>
        [Column]
        public string LastName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Email Address of the person represented by this Speaker record
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Last update time of this Speaker record
        /// </summary>
        [Column]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Latest diagnostic Information for this Speaker record
        /// </summary>
        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
