using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecast.Entities.Abstract;

namespace WeatherForecast.Entities;

[Table("city")]
public class CityEntity : BaseEntity
{
    public CityEntity()
    {
        Id = Guid.NewGuid();
    }
    
    [Column("city_name")]
    public string CityName { get; set; }
}