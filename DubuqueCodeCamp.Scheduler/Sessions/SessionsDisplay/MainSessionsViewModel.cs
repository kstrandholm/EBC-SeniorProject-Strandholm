using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace DubuqueCodeCamp.Scheduler
{
    public class MainSessionsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private DateTime _eventDate = DateTime.Today;

        private List<Session> _sessions = Schedule.GetExistingSessions(DateTime.Today);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<Session> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public MainSessionsViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            // Define Commands
            NavigateCommand = new DelegateCommand<string>(Navigate);

            // Subscribe to Events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Subscribe(RefreshSessions);

            // Publish Events
            // TODO: publish necessary events
        }

        private void GetEventDate(DateTime eventDate)
        {
            _eventDate = eventDate;
            RefreshSessions();
        }

        private void RefreshSessions()
        {
            Sessions = Schedule.GetExistingSessions(_eventDate);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.SessionsRegion, destination);
        }
    }
}
