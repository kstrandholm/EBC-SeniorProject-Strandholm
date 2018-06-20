using System;

namespace DubuqueCodeCamp.DatabaseConnection
{
    public interface IRegistrant : IPerson
    {
        string Occupation { get; set; }

        DateTime? BirthDate { get; set; }
    }
}
