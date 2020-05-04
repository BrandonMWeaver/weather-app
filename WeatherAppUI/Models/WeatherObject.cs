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
            get { return $"{this._cityName}, {this._stateCode}"; }
            set
            {
                this._cityName = value;
            }
        }

        private string _stateCode;

        public string State_Code
        {
            get { return this._stateCode; }
            set
            {
                this._stateCode = value;
            }
        }

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
