using CarTechApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTechApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarTechApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarDetailsPage : ContentPage
    {
        private DetailsViewModel viewModel;
        public CarDetailsPage(CarProp a, string search, int bywhat)
        {
            InitializeComponent();
            viewModel = new DetailsViewModel(a, search, bywhat);
            BindingContext = viewModel;
        }
    }
}