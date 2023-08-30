using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace PubSubApp.Checkout.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {

        await SendPubSubMessage();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private async Task SendPubSubMessage()
    {
        for (int i = 1; i <= 10; i++)
        {
            var order = new Order(i);
            using var client = new DaprClientBuilder().Build();

            // Publish an event/message using Dapr PubSub
            await client.PublishEventAsync("orderpubsub", "orders", order);
            Console.WriteLine("Published data: " + order);
            await client.PublishEventAsync("orderpubsub", "orders-2", order);

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}



public record Order([property: JsonPropertyName("orderId")]
    int OrderId);