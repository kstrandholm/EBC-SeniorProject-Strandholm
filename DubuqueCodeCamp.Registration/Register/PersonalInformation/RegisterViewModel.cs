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
        private readonly IEventAggregator _eventAggregator;

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
                if (SetProperty(ref _firstName, value))
                    _eventAggregator.GetEvent<UpdatedFirstNameEvent>().Publish(_firstName);
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (SetProperty(ref _lastName, value))
                    _eventAggregator.GetEvent<UpdatedLastNameEvent>().Publish(_lastName);
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (SetProperty(ref _emailAddress, value))
                    _eventAggregator.GetEvent<UpdatedEmailEvent>().Publish(_emailAddress);
            }
        }

        private string _occupation;
        public string Occupation
        {
            get => _occupation;
            set
            {
                if (SetProperty(ref _occupation, value))
                    _eventAggregator.GetEvent<UpdatedOccupationEvent>().Publish(_occupation);
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (SetProperty(ref _birthDate, value))
                    _eventAggregator.GetEvent<UpdatedBirthDateEvent>().Publish(_birthDate);
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
            _eventAggregator = eventAggregator;

            // Define Commands
            NextCommand = new DelegateCommand(Execute, CanExecute);
            CancelCommand = new DelegateCommand(Cancel);

            // Subscribe to Events
            _eventAggregator.GetEvent<UpdatedFirstNameEvent>().Subscribe(UpdateRegistrationFirstName);
            _eventAggregator.GetEvent<UpdatedLastNameEvent>().Subscribe(UpdateRegistrationLastName);
            _eventAggregator.GetEvent<UpdatedEmailEvent>().Subscribe(UpdateRegistrationEmail);
            _eventAggregator.GetEvent<UpdatedOccupationEvent>().Subscribe(UpdateRegistrationOccupation);
            _eventAggregator.GetEvent<UpdatedBirthDateEvent>().Subscribe(UpdateRegistrationBirthDate);
            _eventAggregator.GetEvent<UpdatedTalkInterestsEvent>().Subscribe(UpdateTalkInterests);
        }

        private void UpdateRegistrationFirstName(string firstName)
        {
            Registration.FirstName = firstName;
        }

        private void UpdateRegistrationLastName(string lastName)
        {
            Registration.LastName = lastName;
        }

        private void UpdateRegistrationEmail(string emailAddress)
        {
            Registration.EmailAddress = emailAddress;
        }

        private void UpdateRegistrationOccupation(string occupation)
        {
            Registration.Occupation = occupation;
        }

        private void UpdateRegistrationBirthDate(DateTime birthDate)
        {
            Registration.BirthDate = birthDate;
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
