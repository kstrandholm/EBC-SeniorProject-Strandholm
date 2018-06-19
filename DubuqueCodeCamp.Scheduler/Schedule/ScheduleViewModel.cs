using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using DubuqueCodeCamp.DatabaseConnection;

namespace DubuqueCodeCamp.Scheduler
{
    public class ScheduleViewModel : BindableBase
    {
        private DateTime _eventDate = DateTime.Today;

        private List<ProposedSchedule> _schedules = ScheduleOperations.GetProposedSchedule(DateTime.Today);
        public List<ProposedSchedule> Schedules
        {
            get { return _schedules; }
            set { SetProperty(ref _schedules, value); }
        }

        public ScheduleViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
        }

        private void GetEventDate(DateTime eventDate)
        {
            _eventDate = eventDate;
            RefreshSchedule();
        }

        private void RefreshSchedule()
        {
            Schedules = ScheduleOperations.GetProposedSchedule(_eventDate);
        }

        private void GetScheduleFullDetail()
        {
            // TODO: map the schedule from the database to a new class in here that has the info I need
        }
    }
}
