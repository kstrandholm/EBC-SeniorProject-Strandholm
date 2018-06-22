using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;

namespace DubuqueCodeCamp.Registration
{
    /// <inheritdoc />
    /// <summary>
    /// Bootstrapper for the Registration project that initializes and manages the various views
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
            Container.RegisterTypeForNavigation<SplashScreenView>(RegionNames.SplashScreen);
            Container.RegisterTypeForNavigation<PersonalInformation>(RegionNames.RegisterView);
            Container.RegisterTypeForNavigation<TalkInterestsView>(RegionNames.TalkInterestsView);
        }

        /// <inheritdoc />
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MainContentModule));
        }
    }
}
