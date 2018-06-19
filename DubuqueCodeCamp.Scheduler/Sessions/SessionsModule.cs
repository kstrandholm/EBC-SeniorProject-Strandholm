using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class SessionsModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        
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
