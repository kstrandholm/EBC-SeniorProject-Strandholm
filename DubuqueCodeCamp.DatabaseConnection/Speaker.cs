﻿using System;
using System.Data.Linq.Mapping;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Table(Name = "Speakers")]
    public class Speaker : IPerson
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column(CanBeNull = true)]
        public string StreetAddress { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string State { get; set; }

        [Column(CanBeNull = true)]
        public string EmailAddress { get; set; }

        [Column]
        public DateTime UpdateTime { get; set; }

        [Column]
        public string DiagnosticInformation { get; set; }
    }
}
