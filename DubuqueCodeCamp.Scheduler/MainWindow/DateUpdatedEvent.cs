using Prism.Events;
using System;

namespace DubuqueCodeCamp.Scheduler
{
    public class DateUpdatedEvent : PubSubEvent<DateTime>
    {
    }
}
