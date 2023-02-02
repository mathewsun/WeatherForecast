using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers;

public class WeatherController : Controller
{
    private readonly IWeatherForecastService _service;

    public WeatherController(IWeatherForecastService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _service.LoadWeather("Москва");

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index(string? city)
    {
        if (city == null)
        {
            return View(await _service.LoadWeather("Москва"));
        }

        var result = await _service.LoadWeather(city);

        return View(result);
    }
}