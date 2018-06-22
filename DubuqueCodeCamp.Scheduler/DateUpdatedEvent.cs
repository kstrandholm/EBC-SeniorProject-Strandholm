using Prism.Events;
using System;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// Event that indicates a date has been updated
    /// </summary>
    public class DateUpdatedEvent : PubSubEvent<DateTime>
    {
    }
}
