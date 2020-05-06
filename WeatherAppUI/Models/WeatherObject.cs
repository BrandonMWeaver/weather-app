using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUI.Models
{
    class Weather
    {
        public string Description { get; set; }
    }

    class Data
    {
        private string _cityName;

        public string City_Name
        {
            get { return $"{this._cityName}, {this.State_Code}"; }
            set
            {
                this._cityName = value;
            }
        }

        public string State_Code { get; set; }

        private double _temperature;

        public string Temp
        {
            get { return $"{this._temperature * 9 / 5 + 32:N0}\u00B0 F"; }
            set
            {
                this._temperature = double.Parse(value);
            }
        }

        public Weather Weather { get; set; }
    }

    class WeatherObject
    {
        public List<Data> Data { get; set; }
    }
}
