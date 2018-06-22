using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View Model associated with the <see cref="ScheduleView"/>
    /// </summary>
    public class ScheduleViewModel : BindableBase
    {
        private DateTime _eventDate = DateTime.Today;

        private List<TalkSession> _schedule = DatabaseOperations.GetMappedTalkSessions(DateTime.Today);
        /// <summary>
        /// List of <see cref="TalkSession"/> that make up the proposed schedule
        /// </summary>
        public List<TalkSession> Schedule
        {
            get => _schedule;
            set => SetProperty(ref _schedule, value);
        }

        /// <summary>
        /// Constructor for the view model associated with the <see cref="ScheduleView"/>
        /// </summary>
        /// <param name="eventAggregator">Event aggregator created and passed in by Prism/Unity</param>
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
