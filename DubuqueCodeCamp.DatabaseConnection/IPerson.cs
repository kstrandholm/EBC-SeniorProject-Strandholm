namespace DubuqueCodeCamp.DatabaseConnection
{
    /// <summary>
    /// Interface that represents the minimum fields required for a Person class
    /// </summary>
    public interface IPerson
    {
        /// <summary>
        /// First Name of the person
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Last Name of the person
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Email Address of the person
        /// </summary>
        string EmailAddress { get; set; }
    }
}