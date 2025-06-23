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
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weatherData = new WeatherData();
        private bool _isLoading = false;
        private string _errorMessage = string.Empty;

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

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RefreshWeatherCommand { get; set; }

        public WeatherViewModel()
        {
            RefreshWeatherCommand = new Command(async () => await GetCurrentWeatherFromLocation());
            _ = GetCurrentWeatherFromLocation(); // Llamada inicial asíncrona
        }

        public async Task GetCurrentWeatherFromLocation()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                WeatherServices weatherServices = new WeatherServices();
                WeatherDataInfo = await weatherServices.GetCurrentLocationWeatherAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al obtener datos del clima: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}