using CarTechApp.Model;
using CarTechApp.Services;
using CarTechApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarTechApp.ViewModel
{

    public class DetailsViewModel : INotifyPropertyChanged
    {
        IMicroserviceAPI apiService = DependencyService.Get<IMicroserviceAPI>();
        public ICommand saveDateCMD { get; set; }

        private ObservableCollection<CarProp> car;

        public event PropertyChangedEventHandler PropertyChanged;
        public string DateString { get; set; }
        public string Status { get; set; }
        public int IsWorking { get; set; }

        public CarProp actualCar;
        public ObservableCollection<CarProp> Car
        {
            get
            {
                return car;
            }
            set
            {
                SetProperty(ref car, value);
            }
        }

        public string prevsearch { get; set; }
        public int prevbywhat { get; set; }

        public DetailsViewModel(CarProp a,string search, int bywhat)
        {
            prevsearch = search;
            prevbywhat = bywhat;
            saveDateCMD = new Command(async () => await saveDate());
            List<CarProp> b = new List<CarProp>();
            
            b.Add(new CarProp()
            {
                brand = a.brand,
                regNum = a.regNum,
                model = a.model,
                isWorking = a.isWorking,
                actualDate = a.actualDate,
                nextDate = a.nextDate,
            });
            actualCar = (new CarProp()
            {
                brand = a.brand,
                regNum = a.regNum,
                model = a.model,
                isWorking = a.isWorking,
                actualDate = a.actualDate,
                nextDate = a.nextDate,
            });
            Car = new ObservableCollection<CarProp>(b);
        }

        public async Task saveDate()
        {
            await apiService.saveData(actualCar, DateString, IsWorking);
            await App.Current.MainPage.Navigation.PushAsync(new NearbyPage(prevsearch, prevbywhat));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
