using System;
using System.Windows;
using System.Windows.Input;
using DubuqueCodeCamp.DatabaseConnection;
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

        private DateTime _date = DateTime.Today;

        /// <summary>
        /// Title of the talk
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Summary of the talk
        /// </summary>
        private string _summary;
        public string Summary
        {
            get => _summary;
            set => SetProperty(ref _summary, value);
        }

        /// <summary>
        /// First name of the talk's speaker
        /// </summary>
        private string _speakerFirstName;
        public string SpeakerFirstName
        {
            get => _speakerFirstName;
            set => SetProperty(ref _speakerFirstName, value);
        }

        /// <summary>
        /// Last name of the talk's speaker
        /// </summary>
        private string _speakerLastName;
        public string SpeakerLastName
        {
            get => _speakerLastName;
            set => SetProperty(ref _speakerLastName, value);
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
            SaveTalkCommand = new DelegateCommand<string>(SaveTalk, CanExecute)
                .ObservesProperty(() => _date).ObservesProperty(() => Title).ObservesProperty(() => Summary)
                .ObservesProperty(() => SpeakerFirstName).ObservesProperty(() => SpeakerLastName);
            CancelCommand = new DelegateCommand<string>(ReturnToTalks);

            // Subscribe to Events
            _eventAggregator.GetEvent<DateUpdatedEvent>().Subscribe(GetEventDate);
        }

        private void ReturnToTalks(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.TalksRegion, destination);
        }

        private bool CanExecute(string arg)
        {
            return _date != DateTime.MinValue && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Summary) &&
                   !string.IsNullOrEmpty(SpeakerFirstName) && !string.IsNullOrEmpty(SpeakerLastName);
        }

        private void SaveTalk(string destination)
        {
            // Map the current data to the Sessions class
            var newTalk = new TalkInformation()
            {
                TalkDate = _date,
                TalkTitle = Title,
                TalkSummary = Summary,
                SpeakerFirstName = SpeakerFirstName,
                SpeakerLastName = SpeakerLastName
            };

            // Continue as usual only if saving works
            if (DatabaseOperations.SaveTalk(newTalk))
            {
                // Publish the event
                _eventAggregator.GetEvent<TalksUpdatedEvent>().Publish();

                // Navigate to the main Sessions View
                ReturnToTalks(destination);
            }
        }

        private void GetEventDate(DateTime eventDate)
        {
            _date = eventDate;
        }
    }
}
