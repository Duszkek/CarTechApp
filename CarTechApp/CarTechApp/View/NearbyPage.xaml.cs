using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarTechApp.Services;
using CarTechApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CarTechApp.ViewModel;

namespace CarTechApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyPage : ContentPage
    {

        public NearbyPage(string search = "", int bywhat = 0)
        {

            InitializeComponent();
            BindingContext = new NearbyViewModel(this.Navigation, search, bywhat);

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();


        }


    }
}