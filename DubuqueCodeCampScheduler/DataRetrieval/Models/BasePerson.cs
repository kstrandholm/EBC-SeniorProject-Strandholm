using System;

namespace DubuqueCodeCampScheduler
{
    public interface IPerson
    {
        int ID { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
