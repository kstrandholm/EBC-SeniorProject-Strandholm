using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Events;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration.SplashScreen
{
    public class SplashScreenViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private DelegateCommand<string> NavigateCommand { get; set; }

        public SplashScreenViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            // Define Commands
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string destination)
        {
            _regionManager.RequestNavigate("MainRegion", destination);
        }
    }
}
