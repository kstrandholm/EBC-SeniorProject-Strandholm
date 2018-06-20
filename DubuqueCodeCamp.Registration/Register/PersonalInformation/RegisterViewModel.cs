using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ICommand NextCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        
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
            // TODO: Submit the information to the database

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
