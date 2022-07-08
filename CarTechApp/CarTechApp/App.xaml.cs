using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarTechApp.Services;

namespace CarTechApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IMicroserviceAPI, MicroserviceAPI>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
