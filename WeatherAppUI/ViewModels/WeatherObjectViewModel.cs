using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUI.Models;
using WeatherAppUI.ViewModels.ViewModelCommands;
using WeatherAppUI.ViewModels.ViewModelParents;
using Windows.UI.Xaml.Input;

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

        public CommandBase<KeyRoutedEventArgs> GetRequestCommand { get; set; }

        public WeatherObjectViewModel()
        {
            this.WeatherObject = new WeatherObject
            {
                Data = new List<Data>()
            };
            this.GetRequestCommand = new CommandBase<KeyRoutedEventArgs>(GetRequest);
        }

        public void GetRequest(KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Request();
                this.Input = string.Empty;
            }
        }

        public async void Request()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"http://api.weatherbit.io/v2.0/current?city={this.Input}" /* + KEY */);
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();
                    this.WeatherObject = JsonConvert.DeserializeObject<WeatherObject>(data);
                }
            }
            catch(Exception e)
            {
                List<Data> data = new List<Data>
                {
                    new Data { City_Name = "Error", State_Code = e.Message }
                };
                this.WeatherObject = new WeatherObject
                {
                    Data = data
                };
            }
        }
    }
}
