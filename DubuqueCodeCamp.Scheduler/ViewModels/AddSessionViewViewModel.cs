using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class AddSessionViewViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private DateTime _sessionDate;
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

        public AddSessionViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate("SessionsRegion", destination);
        }

    }
}
