using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _eventDate = DateTime.Today;
        public DateTime EventDate
        {
            get { return _eventDate; }
            set
            {
                SetProperty(ref _eventDate, value);
                _eventAggregator.GetEvent<DateUpdatedEvent>().Publish(EventDate);
            }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public ICommand CreateScheduleCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            NavigateCommand = new DelegateCommand<string>(Navigate);
            CreateScheduleCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            throw new NotImplementedException();
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
