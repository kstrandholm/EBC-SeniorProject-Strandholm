using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View Model associated with the <see cref="MainSessionsView"/>
    /// </summary>
    public class MainSessionsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private DateTime _eventDate = DateTime.Today;

        private List<Session> _sessions = DatabaseOperations.GetExistingSessions(DateTime.Today);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<Session> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        /// <summary>
        /// Command to add a new Session and navigate to the Register view
        /// </summary>
        public ICommand AddSessionCommand { get; set; }

        /// <summary>
        /// Command to remove the selected Session
        /// </summary>
        public ICommand RemoveSessionCommand { get; set; }

        /// <summary>
        /// Constructor for the view model associated with the <see cref="MainSessionsView"/>
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event aggregator created and passed in by Prism/Unity</param>
        public MainSessionsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            // Define Commands
            AddSessionCommand = new DelegateCommand<string>(Navigate);

            // Subscribe to Events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Subscribe(RefreshSessions);

            // TODO: implement remove session button
        }

        private void GetEventDate(DateTime eventDate)
        {
            _eventDate = eventDate;
            RefreshSessions();
        }

        private void RefreshSessions()
        {
            Sessions = DatabaseOperations.GetExistingSessions(_eventDate);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.SessionsRegion, destination);
        }
    }
}
