using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _container.RegisterType<MainSessionsView>();

            var region = _regionManager.Regions[RegionNames.ScheduleRegion];
            var scheduleView = _container.Resolve<MainSessionsView>();
            region.Add(scheduleView, RegionNames.ScheduleView);
            region.Activate(scheduleView);
        }
    }
}
