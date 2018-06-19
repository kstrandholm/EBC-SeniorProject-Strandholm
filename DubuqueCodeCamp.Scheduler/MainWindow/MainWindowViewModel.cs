using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View Model for the MainWindow
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private bool _canExecuteCreateSchedule;

        public bool CanExecuteCreateSchedule
        {
            get => _canExecuteCreateSchedule;
            set => SetProperty(ref _canExecuteCreateSchedule, value);
        }

        private DateTime _eventDate = DateTime.Today;
        public DateTime EventDate
        {
            get => _eventDate;
            set
            {
                SetProperty(ref _eventDate, value);
                _eventAggregator.GetEvent<DateUpdatedEvent>().Publish(EventDate);
            }
        }

        /// <summary>
        /// If CanExecute returns true creates the Proposed Schedule and raises the ScheduleCreatedEvent
        /// Otherwise nothing happens
        /// </summary>
        public ICommand CreateScheduleCommand { get; set; }

        /// <summary>
        /// Constructor for the MainWindow view model
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event Aggregator created and passed in by Prism/Unity</param>
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            CreateScheduleCommand = new DelegateCommand(Execute, CanExecute);

            // Subscribe to Events
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Subscribe(UpdatePropertyCanExecute);
        }

        /// <summary>
        /// Update the CanExecuteCreateSchedule to true if it is not already
        /// Prevents redundant assignments
        /// </summary>
        private void UpdatePropertyCanExecute()
        {
            CanExecuteCreateSchedule = true;
        }

        private bool CanExecute()
        {
            return CanExecuteCreateSchedule;
        }

        private void Execute()
        {
            DatabaseOperations.CreateProposedSchedule(EventDate);
            _eventAggregator.GetEvent<ScheduleCreatedEvent>().Publish();
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.SessionsRegion, destination);
        }
    }
}
