using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// View model of the associated Splash Screen view
    /// </summary>
    public class SplashScreenViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Command to begin registration and navigate to the Register view
        /// </summary>
        public ICommand NavigateCommand { get; set; }

        /// <summary>
        /// Constructor for the view model of the Splash Screen view
        /// </summary>
        /// <param name="regionManager">Region manager created and passed in by Prism/Unity</param>
        public SplashScreenViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, destination);
        }
    }
}
