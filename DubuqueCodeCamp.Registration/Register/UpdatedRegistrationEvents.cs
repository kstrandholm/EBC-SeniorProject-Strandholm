using System;
using System.Collections.Generic;
using Prism.Events;

namespace DubuqueCodeCamp.Registration
{
    public class UpdatedFirstNameEvent : PubSubEvent<string>
    {
    }

    public class UpdatedLastNameEvent : PubSubEvent<string>
    {
    }

    public class UpdatedEmailEvent : PubSubEvent<string>
    {
    }

    public class UpdatedOccupationEvent : PubSubEvent<string>
    {
    }

    public class UpdatedBirthDateEvent : PubSubEvent<DateTime>
    {
    }

    public class UpdatedTalkInterestsEvent : PubSubEvent<List<ChosenTalk>>
    {
    }
}
