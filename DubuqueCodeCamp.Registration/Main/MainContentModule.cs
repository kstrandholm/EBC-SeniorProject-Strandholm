using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// Module that handles initializing and managing the Main Content Region
    /// </summary>
    public class MainContentModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Constructor for the Main Content's module that manages the views for the Main Content region
        /// </summary>
        public MainContentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            _container.RegisterType<SplashScreenView>();

            var region = _regionManager.Regions[RegionNames.MainContentRegion];
            var splashScreenView = _container.Resolve<SplashScreenView>();
            region.Add(splashScreenView, RegionNames.SplashScreen);
            region.Activate(splashScreenView);
        }
    }
}
