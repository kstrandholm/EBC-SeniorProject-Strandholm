using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Mvvm;

namespace DubuqueCodeCamp.Scheduler
{
    public class AddSessionViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public AddSessionViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
