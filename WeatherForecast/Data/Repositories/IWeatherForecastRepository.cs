using WeatherForecast.Entities;

namespace WeatherForecast.Data.Repositories
{
    public interface IWeatherForecastRepository
    {
        public Task AddRangeAsync(List<WeatherForecastDayEntity> entities);

        public Task<List<WeatherForecastDayEntity>> GetWeekByCityAsync(string city);
    }
}
