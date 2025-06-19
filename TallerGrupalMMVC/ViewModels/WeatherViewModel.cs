using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static TallerGrupalMMVC.Models.WeatherModels;
using TallerGrupalMMVC.Services;
using System.Windows.Input;

namespace TallerGrupalMMVC.ViewModels
{
    class WeatherViewModel: INotifyPropertyChanged
    {

        private WeatherData _weatherData = new WeatherData();

        public WeatherData WeatherDataInfo
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                }
            }
        }

        public  ICommand RecalculateWeather { get; set; }
        public WeatherViewModel()
        {
            GetCurrentWeatherFromLocation();

        }

        public async void GetCurrentWeatherFromLocation()
        {

            WeatherServices weatherServices = new WeatherServices();
            WeatherDataInfo = await weatherServices.GetCurrentLocatonWeatherAsync();
  
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        

    }
}
