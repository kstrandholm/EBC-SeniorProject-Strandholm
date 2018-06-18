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
    public class AddSessionViewViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _sessionDate = DateTime.Today.AddDays(-5);
        public DateTime SessionDate
        {
            get => _sessionDate;
            set => SetProperty(ref _sessionDate, value);
        }

        private DateTime _timeStart;
        public DateTime TimeStart
        {
            get => _timeStart;
            set => SetProperty(ref _timeStart, value);
        }

        private DateTime _timeEnd;
        public DateTime TimeEnd
        {
            get => _timeEnd;
            set => SetProperty(ref _timeEnd, value);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand UpdateSessionsCommand { get; set; }

        public AddSessionViewViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            UpdateSessionsCommand = new DelegateCommand(Execute, CanExecute);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate("SessionsRegion", destination);
        }

        private bool CanExecute()
        {
            return SessionDate != DateTime.MinValue && SessionDate != DateTime.Today;
        }

        private void Execute()
        {
            SaveChanges();
            _eventAggregator.GetEvent<SessionsUpdatedEvent>().Publish("Updated");
            //NavigateCommand;
        }

        private void SaveChanges()
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
        }
    }
}
