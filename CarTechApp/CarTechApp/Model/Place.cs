using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CarTechApp.Model
{
    public class CarProp
    {
        public string regNum { get; set; }
        public string brand { get; set; }

        public string model { get; set; }
        public DateTime actualDate { get; set; }
        public DateTime nextDate { get; set; }
        public bool isWorking { get; set; }
    }

}
