using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private RegistrationInformation _registration = new RegistrationInformation();
        private RegistrationInformation Registration
        {
            get => _registration;
            set => SetProperty(ref _registration, value);
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                Registration.FirstName = _firstName;
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                Registration.LastName = _lastName;
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                SetProperty(ref _emailAddress, value);
                Registration.EmailAddress = _emailAddress;
            }
        }

        private string _occupation;
        public string Occupation
        {
            get => _occupation;
            set
            {
                SetProperty(ref _occupation, value);
                Registration.Occupation = _occupation;
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                SetProperty(ref _birthDate, value);
                Registration.BirthDate = _birthDate;
            }
        }

        /// <summary>
        /// Command to continue with registration and go to the Talk Interests view
        /// </summary>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// Command to cancel registration and return to the Splash Screen
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Constructor for the view model of the Register view
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        /// <param name="eventAggregator">Event aggregator created and passed in by Prism/Unity</param>
        public RegisterViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;

            // Define Commands
            NextCommand = new DelegateCommand(Execute, CanExecute);
            CancelCommand = new DelegateCommand(Cancel);

            // Subscribe to Events
            eventAggregator.GetEvent<UpdatedTalkInterestsEvent>().Subscribe(UpdateTalkInterests);
        }

        private void UpdateTalkInterests(List<ChosenTalk> chosenTalks)
        {
            Registration.ChosenTalks = chosenTalks;
        }

        private bool CanExecute()
        {
            // TODO: implement real logic
            return true;
        }

        private void Execute()
        {
            // TODO: Pass this information to the next screen to enable 
            // Navigate to the Talk Interest screen to continue registration
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.TalkInterests);

        }

        private void Cancel()
        {
            // Navigate back to the Splash Screen
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.SplashScreen);
        }
    }
}
