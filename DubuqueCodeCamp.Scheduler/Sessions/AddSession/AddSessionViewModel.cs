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
        /// 
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
        /// 
        /// </summary>
        public DateTime TimeStart
        {
            get => _timeStart;
            set => SetProperty(ref _timeStart, value);
        }

        private DateTime _timeEnd = DateTime.Today;
        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeEnd
        {
            get => _timeEnd;
            set => SetProperty(ref _timeEnd, value);
        }

        public ICommand SaveSesssionCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event aggregator created and passed in by Prism/Unity</param>
        public AddSessionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define our commands
            SaveSesssionCommand = new DelegateCommand(Execute, CanExecute)
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

        // TODO: extract this into the DatabaseOperations class
        private void Execute()
        {
            var database = new DCCKellyDatabase();

            // Map the current data to the Sessions class
            var newSession = new Session
            {
                SessionDate = SessionDate,
                TimeStart = TimeStart,
                TimeEnd = TimeEnd,
                UpdateTime = DateTime.Now,
                DiagnosticInformation = "User added session"
            };

            // TODO: consider overriding the equality check on Session?
            // If the database doesn't have any existing sessions matching the new one, save to the database
            if (!database.Sessions.Any(session => session.SessionDate == newSession.SessionDate &&
                                                  session.TimeStart == newSession.TimeStart &&
                                                  session.TimeEnd == newSession.TimeEnd))
            {
                database.Sessions.InsertOnSubmit(newSession);
                database.SubmitChanges();

                // Publish the event for listeners
                _eventAggregator.GetEvent<SessionsUpdatedEvent>().Publish();
            }
            else
            {
                // Otherwise, tell the user one exists and navigate away as usual
                MessageBox.Show("A session with the same date and start and end times already exists in the database. Canceling", "Canceling Save",
                    MessageBoxButton.OK);
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
