using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;

namespace DubuqueCodeCamp.Scheduler
{
    /// <inheritdoc />
    /// <summary>
    /// Bootstrapper for the Scheduler project that initializes and manages the various views
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <inheritdoc />
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <inheritdoc />
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        /// <inheritdoc />
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<MainWindow>(RegionNames.MainWindow);
            Container.RegisterTypeForNavigation<MainSessionsView>(RegionNames.MainSessions);
            Container.RegisterTypeForNavigation<AddSessionView>(RegionNames.AddSession);
            Container.RegisterTypeForNavigation<ScheduleView>(RegionNames.ScheduleView);
        }

        /// <inheritdoc />
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(SessionsModule));
            moduleCatalog.AddModule(typeof(ScheduleModule));
        }
    }
}
