using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View model associated with the <see cref="AddSessionView"/>
    /// </summary>
    public class AddSessionViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _sessionDate;
        /// <summary>
        /// Date the session being added will occur
        /// </summary>
        public DateTime SessionDate
        {
            get => _sessionDate;
            set
            {
                SetProperty(ref _sessionDate, value);
                TimeStart = _sessionDate;
                TimeEnd = _sessionDate;
            }
        }

        private DateTime _timeStart = DateTime.Today;
        /// <summary>
        /// Time the session being added will begin
        /// </summary>
        public DateTime TimeStart
        {
            get => _timeStart;
            set => SetProperty(ref _timeStart, value);
        }

        private DateTime _timeEnd = DateTime.Today;
        /// <summary>
        /// Time the session being added will end
        /// </summary>
        public DateTime TimeEnd
        {
            get => _timeEnd;
            set => SetProperty(ref _timeEnd, value);
        }

        /// <summary>
        /// Command to finish editing and save the new Session, and to return to the <see cref="MainSessionsView"/>
        /// </summary>
        public ICommand SaveSesssionCommand { get; set; }


        /// <summary>
        /// Command to cancel adding a new Session, and to return to the <see cref="MainSessionsView"/>
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Constructor for the view model associated with the <see cref="AddSessionView"/>
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event aggregator created and passed in by Prism/Unity</param>
        public AddSessionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define our commands
            SaveSesssionCommand = new DelegateCommand(SaveSession, CanExecute)
                .ObservesProperty(() => SessionDate).ObservesProperty(() => TimeStart).ObservesProperty(() => TimeEnd);
            CancelCommand = new DelegateCommand(ReturnToMainSessions);

            // Subscribe to events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(SetDefaultSessionDate);
        }

        private void SetDefaultSessionDate(DateTime defaultDate)
        {
            SessionDate = defaultDate;
        }

        private bool CanExecute()
        {
            return SessionDate != DateTime.MinValue && SessionDate != DateTime.Today && TimeStart.Date == SessionDate &&
                   TimeEnd.Date == SessionDate && TimeEnd > TimeStart;
        }

        private void SaveSession()
        {
            // Map the current data to the Sessions class
            var newSession = new Session
            {
                SessionDate = SessionDate,
                TimeStart = TimeStart,
                TimeEnd = TimeEnd,
                UpdateTime = DateTime.Now,
                DiagnosticInformation = "User added session"
            };

            if (DatabaseOperations.SaveSession(newSession))
            {
                // Publish the event
                _eventAggregator.GetEvent<SessionsUpdatedEvent>().Publish();
            }

            // Navigate to the main Sessions View
            ReturnToMainSessions();
        }

        private void ReturnToMainSessions()
        {
            _regionManager.RequestNavigate(RegionNames.SessionsRegion, RegionNames.MainSessions);
        }
    }
}
