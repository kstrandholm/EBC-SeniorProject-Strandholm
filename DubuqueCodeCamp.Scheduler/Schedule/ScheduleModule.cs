using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DubuqueCodeCamp.Scheduler
{
    public class ScheduleModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

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
