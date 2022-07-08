using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CarTechApp.View;

namespace CarTechApp.ViewModel
{
    public class MainViewModel
    {
        public string searchString { get; set; }
        public int radioValue { get; set; }
        public Command GotoCMD { get; set; }
        public MainViewModel()
        {
            GotoCMD = new Command(gotonearby);
        }

        private void gotonearby(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new NearbyPage(searchString, radioValue));
        }

    }
}
