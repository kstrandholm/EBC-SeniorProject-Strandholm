using System;

namespace DubuqueCodeCamp.DatabaseConnection
{
    public interface IRegistrant
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string City { get; set; }

        string State { get; set; }
    }
}
