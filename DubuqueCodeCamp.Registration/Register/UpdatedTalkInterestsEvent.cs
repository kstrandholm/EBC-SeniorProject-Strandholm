using System.Collections.Generic;
using Prism.Events;

namespace DubuqueCodeCamp.Registration
{
    public class UpdatedTalkInterestsEvent : PubSubEvent<List<ChosenTalk>>
    {
    }
}
