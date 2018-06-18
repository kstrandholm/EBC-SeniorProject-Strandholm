using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;

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
            Container.RegisterTypeForNavigation<Sessions>("Sessions");
            //Container.RegisterTypeForNavigation<AddSession>("AddSession");
        }
    }
}
