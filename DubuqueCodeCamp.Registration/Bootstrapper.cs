﻿using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;

namespace DubuqueCodeCamp.Registration
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

        /// <inheritdoc />
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterTypeForNavigation<MainWindow>(RegionNames.MainWindow);
            Container.RegisterTypeForNavigation<SplashScreenView>(RegionNames.SplashScreen);
        }

        /// <inheritdoc />
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }
    }
}