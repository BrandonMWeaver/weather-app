using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUI.Models;
using WeatherAppUI.ViewModels.ViewModelCommands;
using WeatherAppUI.ViewModels.ViewModelParents;
using Windows.Storage;
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

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set
            {
                this._isEnabled = value;
                OnPropertyChanged(nameof(this.IsEnabled));
            }
        }

        public CommandBase<KeyRoutedEventArgs> GetRequestCommand { get; set; }
        public CommandBase<object> SetDefaultCommand { get; set; }

        public WeatherObjectViewModel()
        {
            this.WeatherObject = new WeatherObject
            {
                Data = new List<Data>()
            };
            this.GetRequestCommand = new CommandBase<KeyRoutedEventArgs>(GetRequest);
            this.SetDefaultCommand = new CommandBase<object>(SetDefault);
            this.IsEnabled = false;
            GetDefault();
        }

        public void GetRequest(KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                this.Request();
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
                this.IsEnabled = true;
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

        public async void GetDefault()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("Data.txt");
            string query = await FileIO.ReadTextAsync(file);
            if (query != string.Empty)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"http://api.weatherbit.io/v2.0/current?city={query}" /* + KEY */);
                    response.EnsureSuccessStatusCode();
                    string data = await response.Content.ReadAsStringAsync();
                    this.WeatherObject = JsonConvert.DeserializeObject<WeatherObject>(data);
                }
            }
        }

        public async void SetDefault(object sender)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync("Data.txt", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, this.WeatherObject.Data[0].City_Name);
            this.IsEnabled = false;
        }
    }
}
