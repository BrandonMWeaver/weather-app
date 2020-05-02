using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUI.Models
{
    class Data
    {
        public string City_Name { get; set; }
        public string State_Code { get; set; }
    }

    class WeatherObject
    {
        public List<Data> Data { get; set; }
    }
}
