﻿using DubuqueCodeCamp.DatabaseConnection;
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

        private List<Session> _sessions = DatabaseOperations.GetExistingSessions(DateTime.Today);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<Session> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator"></param>
        public MainSessionsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
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