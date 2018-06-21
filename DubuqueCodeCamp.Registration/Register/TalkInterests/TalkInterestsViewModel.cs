using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// View Model associated with the Talk Interests view
    /// </summary>
    public class TalkInterestsViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private RegistrationInformation _registration;

        private List<ChosenTalk> _chosenTalks = DatabaseOperations.GetChosenTalks();
        /// <summary>
        /// List of Talks and whether they have been chosen by the user or not
        /// </summary>
        public List<ChosenTalk> ChosenTalks
        {
            get => _chosenTalks;
            set => SetProperty(ref _chosenTalks, value);
        }

        /// <summary>
        /// Command to finish registering and return to the Splash Screen
        /// </summary>
        public ICommand SubmitCommand { get; set; }

        /// <summary>
        /// Command to cancel registration and return to the Splash Screen
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Command to leave the view in its current state and go back to the Personal Information view
        /// </summary>
        public ICommand BackCommand { get; set; }

        /// <summary>
        /// Constructor for the view model of the Talk Interests view
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event aggregatory created and passed in by Prism/Unity</param>
        public TalkInterestsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            SubmitCommand = new DelegateCommand<string>(Submit);
            CancelCommand = new DelegateCommand<string>(Cancel);
            BackCommand = new DelegateCommand<string>(Navigate);

            // Subscribe to Events
            _eventAggregator.GetEvent<UpdatedRegistrationEvent>().Subscribe(UpdateRegistration);
            _eventAggregator.GetEvent<ClearRegistrationEvent>().Subscribe(ClearFields);
        }

        private void UpdateRegistration(RegistrationInformation registration)
        {
            _registration = registration;
        }

        private void ClearFields()
        {
            ChosenTalks = DatabaseOperations.GetChosenTalks();
        }

        private void Submit(string destination)
        {
            // TODO: Verify submited all the information to the database
            _registration.ChosenTalks = ChosenTalks;
            DatabaseOperations.SaveRegistration(_registration);

            ClearFieldsAndNavigate(destination);

            MessageBox.Show("Registration Complete", "Registration Complete", MessageBoxButton.OK);
        }

        private void Cancel(string destination)
        {
            var result = MessageBox.Show("This will discard all your changes. Are you sure?", "Are you sure?", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
                ClearFieldsAndNavigate(destination);
        }

        private void ClearFieldsAndNavigate(string destination)
        {
            _eventAggregator.GetEvent<ClearRegistrationEvent>().Publish();

            Navigate(destination);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, destination);
        }
    }
}
