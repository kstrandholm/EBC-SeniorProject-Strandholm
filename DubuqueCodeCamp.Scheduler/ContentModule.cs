using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// Module that handles initializing and managing the regions in this project
    /// </summary>
    public class ContentModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Constructor for the Session's module that manages the views for the Sessions region
        /// </summary>
        /// <param name="container">Unity Dependency Injection container created and passed in by Unity</param>
        /// <param name="regionManager">Region Manager created and passed in by Prism/Unity</param>
        public ContentModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            // Initialize the Sessions Region
            _container.RegisterType<MainSessionsView>();

            var sessionsRegion = _regionManager.Regions[RegionNames.SessionsRegion];
            var mainSessionsView = _container.Resolve<MainSessionsView>();
            sessionsRegion.Add(mainSessionsView, RegionNames.MainSessions);
            sessionsRegion.Activate(mainSessionsView);

            // Initialize the Talks Region
            _container.RegisterType<MainSessionsView>();

            var talksRegion = _regionManager.Regions[RegionNames.TalksRegion];
            var talksView = _container.Resolve<TalksView>();
            talksRegion.Add(talksView, RegionNames.TalksView);
            talksRegion.Activate(talksView);

            // Initialize the Schedule Region
            _container.RegisterType<ScheduleView>();

            var region = _regionManager.Regions[RegionNames.ScheduleRegion];
            var scheduleView = _container.Resolve<ScheduleView>();
            region.Add(scheduleView, RegionNames.ScheduleView);
            region.Activate(scheduleView);
        }
    }
}
