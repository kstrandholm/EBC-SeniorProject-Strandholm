using System;
using Prism.Events;
using Prism.Mvvm;

namespace DubuqueCodeCamp.Scheduler
{
    public class AddSessionViewViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private DateTime _sessionDate;
        public DateTime SessionDate
        {
            get => _sessionDate;
            set => SetProperty(ref _sessionDate, value);
        }

        private DateTime _timeStart;
        public DateTime TimeStart
        {
            get => _timeStart;
            set => SetProperty(ref _timeStart, value);
        }

        private DateTime _timeEnd;
        public DateTime TimeEnd
        {
            get => _timeEnd;
            set => SetProperty(ref _timeEnd, value);
        }

        public AddSessionViewViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

    }
}
