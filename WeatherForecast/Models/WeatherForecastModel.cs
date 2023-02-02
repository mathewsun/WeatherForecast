namespace WeatherForecast.Models;

public class WeatherForecastModel
{
    public string City { get; set; }
    public ForecastDayModel CurrentForecast { get; set; }
    public List<ForecastDayModel> ForecastDayModels { get; set; }
}

public class ForecastDayModel
{
    public double TempC { get; set; }
    public string ConditionIconUrl { get; set; }
    public double WindKph { get; set; }
    public double ChanceOfRain { get; set; }
    public DateTime DateTime { get; set; }
}