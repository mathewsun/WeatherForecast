using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.Entities.Abstract;

public abstract class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }
}