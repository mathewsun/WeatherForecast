using Microsoft.EntityFrameworkCore;
using WeatherForecast.Entities;

namespace WeatherForecast.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<CityEntity> CityEntities { get; set; }
    public DbSet<WeatherForecastDayEntity> WeatherForecastDayEntities { get; set; }
}