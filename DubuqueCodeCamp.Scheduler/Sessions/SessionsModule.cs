using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// Module that handles initializing and managing the Sessions Region
    /// </summary>
    public class SessionsModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Constructor for the Session's module that manages the views for the Sessions region
        /// </summary>
        /// <param name="container">Unity Dependency Injection container created and passed in by Unity</param>
        /// <param name="regionManager">Region Manager created and passed in by Prism/Unity</param>
        public SessionsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            _container.RegisterType<MainSessionsView>();

            var region = _regionManager.Regions[RegionNames.SessionsRegion];
            var mainSessionsView = _container.Resolve<MainSessionsView>();
            region.Add(mainSessionsView, RegionNames.MainSessions);
            region.Activate(mainSessionsView);
        }
    }
}
