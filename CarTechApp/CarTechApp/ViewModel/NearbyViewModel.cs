using CarTechApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using CarTechApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using CarTechApp.View;

namespace CarTechApp.ViewModel  
{
    public class NearbyViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CarProp Item { get; set; }

        private ObservableCollection<CarProp> carList;
        public ObservableCollection<CarProp> CarList
        {
            get
            {
                return carList;
            }
            set
            {
                SetProperty(ref carList, value);
            }
        }


        public ICommand RefreshCommand { get; set; }
        public ICommand GoToPage { get; set; }

        IMicroserviceAPI apiService = DependencyService.Get<IMicroserviceAPI>();
        INavigation navigation;
        public NearbyViewModel(INavigation nav, string search, int bywhat)
        {
            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
            navigation = nav;
            CarList = new ObservableCollection<CarProp>();
            RefreshCommand = new Command(async () => await RefreshList(search, bywhat));
            GoToPage = new Command(async () => await GoToDetails(search, bywhat));
            Task.Run(async () => await RefreshList(search, bywhat));
            //List<CarProp> a = new List<CarProp>(); 
            //a.Add(new CarProp()
            //{
            //    brand = "City",
            //    regNum = "2020",
            //    model = "Pablo",
            //    isWorking = true,
            //});
            //CarList = new ObservableCollection<CarProp>(a);
            activityIndicator.IsRunning = false;
        }

        public async Task GoToDetails(string search, int bywhat)
        {
           await navigation.PushAsync(new CarDetailsPage(Item, search, bywhat));
        }
        public async Task RefreshList(string search, int bywhat)
        {
            CarList.Clear();
            var data = await apiService.getData(search, bywhat);
            CarList = new ObservableCollection<CarProp>(data);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
