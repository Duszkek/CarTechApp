using CarTechApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CarTechApp.Services
{
    public interface IMicroserviceAPI
    {
       Task <List<CarProp>> getData(string model, int bywhat);
       Task saveData(CarProp car, string dateasstring, int isworking);
    }
}
