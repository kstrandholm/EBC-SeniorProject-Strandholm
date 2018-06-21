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
    /// View model associated with the <see cref="AddTalkView" />
    /// </summary>
    public class AddTalkViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _summary;
        public string Summary
        {
            get => _summary;
            set => SetProperty(ref _summary, value);
        }

        private string _speaker;
        public string Speaker
        {
            get => _speaker;
            set => SetProperty(ref _speaker, value);
        }

        /// <summary>
        /// Command to save the talk to the database and navigate back to the talks view
        /// </summary>
        public ICommand SaveTalkCommand { get; set; }

        /// <summary>
        /// Command to cancel adding a talk and navigate back to the talks view
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Constructor of the view model associated with the <see cref="AddTalkView" />
        /// </summary>
        public AddTalkViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            SaveTalkCommand = new DelegateCommand<string>(SaveTalk, CanExecute);

            // Subscribe to Events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
        }

        private bool CanExecute(string arg)
        {
            throw new System.NotImplementedException();
        }

        private void SaveTalk(string destination)
        {
            _eventAggregator.GetEvent<TalksUpdatedEvent>().Publish();
            _regionManager.RequestNavigate(RegionNames.TalksRegion, destination);
        }

        private void GetEventDate(DateTime eventDate)
        {
            Date = eventDate;
        }
    }
}
