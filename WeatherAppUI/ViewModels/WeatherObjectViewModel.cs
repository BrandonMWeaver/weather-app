using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUI.Models;
using WeatherAppUI.ViewModels.ViewModelParents;

namespace WeatherAppUI.ViewModels
{
    class WeatherObjectViewModel : NotificationBase
    {
        private WeatherObject _weatherObject;

        public WeatherObject WeatherObject
        {
            get { return this._weatherObject; }
            set
            {
                this._weatherObject = value;
                this.OnPropertyChanged(nameof(this.WeatherObject));
            }
        }

        private string _input;

        public string Input
        {
            get { return this._input; }
            set
            {
                this._input = value;
                this.OnPropertyChanged(nameof(this.Input));
            }
        }

        public WeatherObjectViewModel()
        {
            this.WeatherObject = new WeatherObject
            {
                Data = new List<Data>()
            };
        }
    }
}
