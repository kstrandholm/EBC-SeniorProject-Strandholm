using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;

namespace DubuqueCodeCamp.Scheduler
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<MainWindow>("MainWindow");
            Container.RegisterTypeForNavigation<MainSessionsView>("MainSessionsView");
            Container.RegisterTypeForNavigation<AddSessionView>("AddSessionView");
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(SessionsModule));
        }
    }
}
