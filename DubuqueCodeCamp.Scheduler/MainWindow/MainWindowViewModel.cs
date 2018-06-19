using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Windows.Input;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View Model for the MainWindow
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private bool _canExecuteCreateSchedule;
        private DateTime _eventDate = DateTime.Today;

        /// <summary>
        /// Using a variable backed property here prevents redundant assignments to CanExecuteCreateSchedule
        /// </summary>
        public bool CanExecuteCreateSchedule
        {
            get => _canExecuteCreateSchedule;
            set => SetProperty(ref _canExecuteCreateSchedule, value);
        }

        public DateTime EventDate
        {
            get => _eventDate;
            set
            {
                SetProperty(ref _eventDate, value);
                _eventAggregator.GetEvent<DateUpdatedEvent>().Publish(EventDate);
                UpdatePropertyCanExecute();
            }
        }

        /// <summary>
        /// If CanExecute returns true creates the Proposed Schedule and publishes the ScheduleCreatedEvent
        /// Otherwise nothing happens
        /// </summary>
        public ICommand CreateScheduleCommand { get; set; }

        /// <summary>
        /// Constructor for the MainWindow view model
        /// </summary>
        /// <param name="eventAggregator">Event Aggregator created and passed in by Prism/Unity</param>
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Define Commands
            CreateScheduleCommand = new DelegateCommand(Execute).ObservesCanExecute(() => CanExecuteCreateSchedule);

            // Subscribe to Events
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Subscribe(UpdatePropertyCanExecute);
        }

        /// <summary>
        /// Update the CanExecuteCreateSchedule to true if there are existing sessions for the EventDate
        /// </summary>
        private void UpdatePropertyCanExecute()
        {
            CanExecuteCreateSchedule = DatabaseOperations.GetExistingSessions(_eventDate).Any();
        }

        private void Execute()
        {
            var scheduleSaved = DatabaseOperations.CreateProposedSchedule(EventDate);

            // Only publish the event if the schedule was created and saved
            if (scheduleSaved)
                _eventAggregator.GetEvent<ScheduleCreatedEvent>().Publish();
        }
    }
}
