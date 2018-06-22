using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// View model asociated with the <see cref="TalksView" />
    /// </summary>
    public class TalksViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private DateTime _eventDate = DateTime.Today;

        private List<TalkInformation> _talks = DatabaseOperations.GetTalkInformation(DateTime.Today);
        /// <summary>
        /// List of <see cref="TalkInformation"/> that contains all the information about a talk to be displayed
        /// </summary>
        public List<TalkInformation> Talks
        {
            get => _talks;
            set => SetProperty(ref _talks, value);
        }

        /// <summary>
        /// Holds the information of the currently selected talk
        /// </summary>
        private TalkInformation _talkInformation;
        public TalkInformation TalkInformation
        {
            get => _talkInformation;
            set => SetProperty(ref _talkInformation, value);
        }

        /// <summary>
        /// Command to add a new talk
        /// </summary>
        public ICommand AddTalkCommand { get; set; }

        /// <summary>
        /// Command to remove the currently selected talk
        /// </summary>
        public ICommand RemoveTalkCommand { get; set; }

        /// <summary>
        /// Constructor for the view model asociated with the <see cref="TalksView" />
        /// </summary>
        public TalksViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            AddTalkCommand = new DelegateCommand<string>(AddTalk);
            RemoveTalkCommand = new DelegateCommand(RemoveTalk);

            // Subscribe to Events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
            _eventAggregator.GetEvent<TalksUpdatedEvent>().Subscribe(RefreshTalks);
        }

        private void AddTalk(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.TalksRegion, destination);
        }

        private void RemoveTalk()
        {
            DatabaseOperations.RemoveTalk(TalkInformation);
            _eventAggregator.GetEvent<TalksUpdatedEvent>().Publish();
        }

        private void GetEventDate(DateTime eventDate)
        {
            _eventDate = eventDate;
            RefreshTalks();
        }

        private void RefreshTalks()
        {
            Talks = DatabaseOperations.GetTalkInformation(_eventDate);
        }
    }
}
