﻿using DubuqueCodeCamp.DatabaseConnection;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class MainSessionsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

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
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate("SessionsRegion", destination);
        }
    }
}
