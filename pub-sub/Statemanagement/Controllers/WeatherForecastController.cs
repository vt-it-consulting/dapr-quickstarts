using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Statemanagement.Models;

namespace Statemanagement.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly DaprClient _daprClient;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        DaprClient daprClient,
        ILogger<WeatherForecastController> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<ActionResult<WeatherForecast>> Post(WeatherForecast weatherForecast)
    {
        await _daprClient
            .SaveStateAsync("statestore", "AMS", weatherForecast);

        return Ok(weatherForecast);
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<WeatherForecast>> Get()
    {
        var weatherForecast = await _daprClient
            .GetStateAsync<WeatherForecast>("statestore", "AMS");

        return Ok(weatherForecast);
    }
}
