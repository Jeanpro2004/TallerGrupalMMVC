using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TallerGrupalMMVC.Models.WeatherModels;

namespace TallerGrupalMMVC.Services
{
    public class WeatherServices
    {
        public async Task<WeatherData> GetCurrentLocationWeatherAsync()
        {
            GeoLocationServices geoLocationServices = new GeoLocationServices();
            Location location = await geoLocationServices.GetCurrentLocation();

            return await GetWeatherDataFromLocationAsync(location.Latitude, location.Longitude);
        }

        public async Task<WeatherData> GetWeatherDataFromLocationAsync(double latitude, double longitude)
        {
            try
            {
                string latitude_str = latitude.ToString("F4", System.Globalization.CultureInfo.InvariantCulture);
                string longitude_str = longitude.ToString("F4", System.Globalization.CultureInfo.InvariantCulture);

                // URL corregida según los requisitos
                string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude_str}&longitude={longitude_str}&current=temperature_2m,relative_humidity_2m,rain";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    WeatherData data = JsonConvert.DeserializeObject<WeatherData>(result);
                    return data;
                }
            }
            catch (Exception ex)
            {
                // En caso de error, devolver datos por defecto
                return new WeatherData
                {
                    current = new Current
                    {
                        time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm"),
                        temperature_2m = 0.0,
                        relative_humidity_2m = 0,
                        rain = 0.0
                    },
                    current_units = new CurrentUnits
                    {
                        time = "iso8601",
                        temperature_2m = "°C",
                        relative_humidity_2m = "%",
                        rain = "mm"
                    }
                };
            }
        }
    }
}