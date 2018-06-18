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
            _container.RegisterType<SessionsDisplayer>();
            //_regionManager.RegisterViewWithRegion("SessionsRegion", typeof(SessionsDisplayer));

            var region = _regionManager.Regions["SessionsRegion"];
            var sessionsDisplayer = _container.Resolve<SessionsDisplayer>();
            region.Add(sessionsDisplayer, "SessionsDisplayer");
            region.Activate(sessionsDisplayer);
        }
    }
}
