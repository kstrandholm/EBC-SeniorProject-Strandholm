using Prism.Events;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// Event that indicates the Registration object on a receiving class should be updated with the passed information
    /// </summary>
    public class UpdatedRegistrationEvent : PubSubEvent<RegistrationInformation>
    {
    }
}
