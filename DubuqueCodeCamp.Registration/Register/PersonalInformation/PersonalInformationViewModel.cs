using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// View model associated to the Register view
    /// </summary>
    public class PersonalInformationViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        private string _firstName;
        /// <summary>
        /// First name of the currently registering person
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        /// <summary>
        /// Last name of the currently registering person. Required field
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _emailAddress;
        /// <summary>
        /// Email Address of the currently registering person. Required field
        /// </summary>
        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }

        private string _occupation;
        /// <summary>
        /// Occupation of the currently registering person. Optional field
        /// </summary>
        public string Occupation
        {
            get => _occupation;
            set => SetProperty(ref _occupation, value);
        }

        private DateTime? _birthDate;
        /// <summary>
        /// Birth date of the currently registering person. Optional field
        /// </summary>
        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
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
        public PersonalInformationViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            // Define Commands
            NextCommand = new DelegateCommand(Next, CanExecute)
                .ObservesProperty(() => FirstName).ObservesProperty(() => LastName).ObservesProperty(() => EmailAddress);
            CancelCommand = new DelegateCommand<string>(Cancel);

            // Subscribe to Events
            _eventAggregator.GetEvent<ClearRegistrationEvent>().Subscribe(ClearFields);
        }

        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            EmailAddress = string.Empty;
            Occupation = string.Empty;
            BirthDate = null;
        }

        private bool CanExecute()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(EmailAddress);
        }

        private void Next()
        {
            var registration = new RegistrationInformation
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                BirthDate = BirthDate,
                Occupation = Occupation
            };
            _eventAggregator.GetEvent<UpdatedRegistrationEvent>().Publish(registration);

            // Navigate to the Talk Interest screen to continue registration
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.TalkInterestsView);

        }

        private void Cancel(string destination)
        {
            // Clear the fields
            _eventAggregator.GetEvent<ClearRegistrationEvent>().Publish();

            // Navigate back to the Splash Screen
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, destination);
        }
    }
}
