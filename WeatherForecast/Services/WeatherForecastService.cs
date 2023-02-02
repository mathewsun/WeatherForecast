using Flurl.Http;
using WeatherForecast.Data.Repositories;
using WeatherForecast.Entities;
using WeatherForecast.Models;
using WeatherForecast.Models.Requests;

namespace WeatherForecast.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task<WeatherForecastModel> LoadWeather(string city)
    {
        var forecasts = await _weatherForecastRepository.GetWeekByCityAsync(city);

        WeatherForecastModel model;
        
        if (forecasts.Count < 7)
        {
            var weekData = await "http://api.weatherapi.com/v1/forecast.json?key=850d693301c046a1a8f133425230202&q={city}&days=7&hour=12&lang=ru"
                .Replace("{city}", city)
                .GetJsonAsync<GetWeatherRequestModel>();
            
            var newForecasts = weekData.forecast.forecastday
                .Skip(forecasts.Count)
                .Select(x => new WeatherForecastDayEntity()
                {
                    City = new CityEntity()
                    {
                        CityName = city,
                    },
                    TempC = x.hour.FirstOrDefault()!.temp_c,
                    ChanceOfRain = x.hour.FirstOrDefault()!.chance_of_rain,
                    WindKph = x.hour.FirstOrDefault()!.wind_kph,
                    ConditionIconUrl = x.hour.FirstOrDefault()!.condition.icon,
                    DateTime = DateTime.ParseExact(x.date,  "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                }).ToList();

            await _weatherForecastRepository.AddRangeAsync(newForecasts);

            if (forecasts.Count == 0)
            {
                model = new WeatherForecastModel()
                {
                    City = city,
                    CurrentForecast = new ForecastDayModel()
                    {
                        TempC = weekData.current.temp_c,
                        ConditionIconUrl = weekData.current.condition.icon,
                        ChanceOfRain = weekData.forecast.forecastday.FirstOrDefault()!.hour.FirstOrDefault()!.chance_of_rain,
                        DateTime = DateTime.ParseExact(weekData.forecast.forecastday.FirstOrDefault()!.date,"yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        WindKph = weekData.current.wind_kph
                    },
                    ForecastDayModels = weekData.forecast.forecastday.Skip(1).Select(x => new ForecastDayModel()
                    {
                        TempC = x.hour.FirstOrDefault()!.temp_c,
                        ConditionIconUrl = x.hour.FirstOrDefault()!.condition.icon,
                        ChanceOfRain = x.hour.FirstOrDefault()!.chance_of_rain,
                        WindKph = x.hour.FirstOrDefault()!.wind_kph,
                        DateTime = DateTime.ParseExact(x.date,  "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    }).ToList()
                };
            }
            else
            {
                model = new WeatherForecastModel()
                {
                    City = city,
                    CurrentForecast = new ForecastDayModel()
                    {
                        TempC = forecasts.FirstOrDefault()!.TempC,
                        ConditionIconUrl = forecasts.FirstOrDefault()!.ConditionIconUrl,
                        ChanceOfRain = forecasts.FirstOrDefault()!.ChanceOfRain,
                        DateTime = forecasts.FirstOrDefault()!.DateTime,
                        WindKph = forecasts.FirstOrDefault()!.WindKph
                    },
                    ForecastDayModels = forecasts.Skip(1).Select(x => new ForecastDayModel()
                    {
                        TempC = x.TempC,
                        ConditionIconUrl = x.ConditionIconUrl,
                        ChanceOfRain = x.ChanceOfRain,
                        WindKph = x.WindKph,
                        DateTime = x.DateTime
                    }).ToList()
                };
                
                model.ForecastDayModels.AddRange(weekData.forecast.forecastday.Skip(1).Select(x => new ForecastDayModel()
                {
                    TempC = x.hour.FirstOrDefault()!.temp_c,
                    ConditionIconUrl = x.hour.FirstOrDefault()!.condition.icon,
                    ChanceOfRain = x.hour.FirstOrDefault()!.chance_of_rain,
                    WindKph = x.hour.FirstOrDefault()!.wind_kph,
                    DateTime = DateTime.ParseExact(x.date,  "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                }).ToList());
            }
        }
        else
        {
            model = new WeatherForecastModel()
            {
                City = city,
                CurrentForecast = new ForecastDayModel()
                {
                    TempC = forecasts.FirstOrDefault()!.TempC,
                    ConditionIconUrl = forecasts.FirstOrDefault()!.ConditionIconUrl,
                    ChanceOfRain = forecasts.FirstOrDefault()!.ChanceOfRain,
                    DateTime = forecasts.FirstOrDefault()!.DateTime,
                    WindKph = forecasts.FirstOrDefault()!.WindKph
                },
                ForecastDayModels = forecasts.Skip(1).Select(x => new ForecastDayModel()
                {
                    TempC = x.TempC,
                    ConditionIconUrl = x.ConditionIconUrl,
                    ChanceOfRain = x.ChanceOfRain,
                    WindKph = x.WindKph,
                    DateTime = x.DateTime
                }).ToList()
            };
        }

        return model;
    }
}