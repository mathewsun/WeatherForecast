using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecast.Entities.Abstract;

namespace WeatherForecast.Entities;

public class WeatherForecastDayEntity : BaseEntity
{
    public WeatherForecastDayEntity()
    {
        Id = Guid.NewGuid();
    }
    
    [ForeignKey("city_id")]
    public CityEntity City { get; set; }
    [Column("temp_c")]
    public double TempC { get; set; }
    [Column("condition_icon_url")]
    public string ConditionIconUrl { get; set; }
    [Column("wind_kph")]
    public double WindKph { get; set; }
    [Column("chacne_of_rain")]
    public double ChanceOfRain { get; set; }
    [Column("datetime")]
    public DateTime DateTime { get; set; }
}