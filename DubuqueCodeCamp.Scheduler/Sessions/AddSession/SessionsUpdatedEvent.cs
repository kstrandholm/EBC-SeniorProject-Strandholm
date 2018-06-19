using DubuqueCodeCamp.DatabaseConnection;
using Prism.Events;

namespace DubuqueCodeCamp.Scheduler
{
    public class SessionsUpdatedEvent : PubSubEvent<Session>
    {
    }
}
