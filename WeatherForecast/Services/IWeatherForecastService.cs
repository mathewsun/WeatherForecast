using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IWeatherForecastService
    {
        public Task<WeatherForecastModel> LoadWeather(string city);
    }
}
