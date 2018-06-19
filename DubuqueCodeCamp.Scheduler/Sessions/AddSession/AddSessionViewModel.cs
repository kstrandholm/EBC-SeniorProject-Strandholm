using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using PropertyChanged;

namespace DubuqueCodeCamp.Scheduler
{
    [AddINotifyPropertyChangedInterface]
    public class AddSessionViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _sessionDate = DateTime.Today;
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
        public DateTime TimeStart
        {
            get => _timeStart;
            set => SetProperty(ref _timeStart, value);
        }

        private DateTime _timeEnd = DateTime.Today;
        public DateTime TimeEnd
        {
            get => _timeEnd;
            set => SetProperty(ref _timeEnd, value);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand SaveSesssionCommand { get; set; }

        public AddSessionViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define our commands
            NavigateCommand = new DelegateCommand<string>(Navigate);
            SaveSesssionCommand = new DelegateCommand(Execute, CanExecute)
                .ObservesProperty(() => SessionDate).ObservesProperty(() => TimeStart).ObservesProperty(() => TimeEnd);

            // Subscribe to events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(SetDefaultSessionDate);
        }

        private void SetDefaultSessionDate(DateTime defaultDate)
        {
            SessionDate = defaultDate;
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate("SessionsRegion", destination);
        }

        private bool CanExecute()
        {
            return SessionDate != DateTime.MinValue && SessionDate != DateTime.Today && TimeStart.Date == SessionDate &&
                   TimeEnd.Date == SessionDate && TimeEnd > TimeStart;
        }

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

            database.Sessions.InsertOnSubmit(newSession);

            // Publish the event for listeners
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Publish("Updated");

            // Navigate to the main Sessions View
            //NavigateCommand;
        }
    }
}
