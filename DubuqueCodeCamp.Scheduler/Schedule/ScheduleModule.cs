using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// Module that handles initializing and managing the Schedule Region
    /// </summary>
    public class ScheduleModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Constructor for the Schedule's module that manages the views for the Schedule region
        /// </summary>
        /// <param name="container">Unity Dependency Injection container created and passed in by Unity</param>
        /// <param name="regionManager">Region Manager created and passed in by Prism/Unity</param>
        public ScheduleModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            _container.RegisterType<ScheduleView>();

            var region = _regionManager.Regions[RegionNames.ScheduleRegion];
            var scheduleView = _container.Resolve<ScheduleView>();
            region.Add(scheduleView, RegionNames.ScheduleView);
            region.Activate(scheduleView);
        }
    }
}
