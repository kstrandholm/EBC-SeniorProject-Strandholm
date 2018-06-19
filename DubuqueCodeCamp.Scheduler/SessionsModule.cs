using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class SessionsModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        
        public SessionsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            _container.RegisterType<MainSessionsView>();
            //_regionManager.RegisterViewWithRegion("SessionsRegion", typeof(MainSessionsView));

            var region = _regionManager.Regions["SessionsRegion"];
            var mainSessionsView = _container.Resolve<MainSessionsView>();
            region.Add(mainSessionsView, "MainSessionsView");
            region.Activate(mainSessionsView);
        }
    }
}
