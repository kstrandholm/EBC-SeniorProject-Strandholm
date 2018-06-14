using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace DubuqueCodeCamp.DatabaseConnection
{
    [Table]
    public class TalkInterest
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public int TalkID { get; set; }

        [Column]
        public int InterestedRegistrantID { get; set; }
    }
}
