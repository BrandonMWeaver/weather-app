using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUI.Models
{
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
    }

    class WeatherObject
    {
        public List<Data> Data { get; set; }
    }
}
