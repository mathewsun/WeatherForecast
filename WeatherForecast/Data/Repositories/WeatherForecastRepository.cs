using Microsoft.EntityFrameworkCore;
using WeatherForecast.Entities;

namespace WeatherForecast.Data.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ApplicationDbContext _dbContext;
    public WeatherForecastRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(List<WeatherForecastDayEntity> entities)
    {
        await _dbContext.WeatherForecastDayEntities.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<WeatherForecastDayEntity>> GetWeekByCityAsync(string city)
    {
        var forecasts = _dbContext.WeatherForecastDayEntities
            .Where(x => x.City.CityName == city && x.DateTime >= DateTime.Now.Date);

        return await forecasts.ToListAsync();
    }
}