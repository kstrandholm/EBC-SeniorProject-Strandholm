using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

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
        public RegisterViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            NextCommand = new DelegateCommand(Execute, CanExecute);
            CancelCommand = new DelegateCommand(Cancel);
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
