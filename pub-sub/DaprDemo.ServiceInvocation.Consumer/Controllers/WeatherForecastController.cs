using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprDemo.ServiceInvocation.Consumer.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DaprClient _daprClient;

    public WeatherForecastController(
        DaprClient daprClient,
        ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {

        var res = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
            HttpMethod.Get,
            "service-weather-api",
            "weather-forecast/all"
          );

        return Ok(res);
    }
}
