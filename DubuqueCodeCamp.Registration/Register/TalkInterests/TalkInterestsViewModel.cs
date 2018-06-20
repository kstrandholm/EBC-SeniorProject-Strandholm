using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// View Model linked to the Talk Interests view
    /// </summary>
    public class TalkInterestsViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Command to finish registering and return to the Splash Screen
        /// </summary>
        public ICommand SubmitCommand { get; set; }

        /// <summary>
        /// Command to cancel registration and return to the Splash Screen
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Command to save the current state and go back to the Personal Information view
        /// </summary>
        public ICommand BackCommand { get; set; }

        /// <summary>
        /// Constructor for the view model of the Talk Interests view
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        public TalkInterestsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            SubmitCommand = new DelegateCommand(Execute, CanExecute);
            CancelCommand = new DelegateCommand(Cancel);
            BackCommand = new DelegateCommand(Back);
        }

        private bool CanExecute()
        {
            // TODO: implement real logic?
            return true;
        }

        private void Execute()
        {
            // TODO: Submit the information to the database

            NavigateToSplashScreen();

            MessageBox.Show("Registration Complete", "Registration Complete", MessageBoxButton.OK);
        }

        private void Cancel()
        {
            // TODO: warn user this will cancel all registration

            NavigateToSplashScreen();
        }

        private void NavigateToSplashScreen()
        {
            // TODO: Clear the information on this and the Register screen

            // Navigate back to the Splash Screen
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.SplashScreen);
        }

        private void Back()
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.RegisterView);
        }
    }
}
