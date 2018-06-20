using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public ICommand SubmitCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        
        public RegisterViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            SubmitCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            // TODO: implement real logic
            return true;
        }

        private void Execute()
        {
            // TODO: Submit the information to the database

            // Navigate back to the splash screen
            Navigate();
        }

        private void Navigate()
        {
            // Navigate back to the Splash Screen
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, RegionNames.SplashScreen);
        }
    }
}
