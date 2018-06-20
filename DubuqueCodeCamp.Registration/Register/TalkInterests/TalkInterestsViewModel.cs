using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    public class TalkInterestsViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ICommand SubmitCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public TalkInterestsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            SubmitCommand = new DelegateCommand(Execute, CanExecute);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private bool CanExecute()
        {
            // TODO: implement real logic
            return true;
        }

        private void Execute()
        {
            // TODO: Submit the information to the database

            // Navigate to the Talk Interest screen to finish registration
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.SplashScreen);

        }

        private void Cancel()
        {
            // TODO: warn user this will cancel all registration
            // Navigate back to the Splash Screen
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.SplashScreen);
        }
    }
}
