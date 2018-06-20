namespace DubuqueCodeCamp.DatabaseConnection
{
    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string EmailAddress { get; set; }
    }
}