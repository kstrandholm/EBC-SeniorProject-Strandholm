using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DubuqueCodeCamp.Scheduler
{
    public class ScheduleViewModel : BindableBase
    {
        private DateTime _eventDate = DateTime.Today;

        private List<TalkSession> _schedule = DatabaseOperations.GetMappedTalkSessions(DateTime.Today);
        public List<TalkSession> Schedule
        {
            get => _schedule;
            set => SetProperty(ref _schedule, value);
        }

        public ScheduleViewModel(IEventAggregator eventAggregator)
        {
            // Subscribe to Events
            eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
            eventAggregator.GetEvent<ScheduleCreatedEvent>().Subscribe(RefreshSchedule);
        }

        private void GetEventDate(DateTime eventDate)
        {
            _eventDate = eventDate;
            RefreshSchedule();
        }

        private void RefreshSchedule()
        {
            Schedule = DatabaseOperations.GetMappedTalkSessions(_eventDate);
        }
    }
}
