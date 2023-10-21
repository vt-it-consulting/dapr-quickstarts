using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using PubSubWithDapr.Checkout.Models;
using PubSubWithDapr.Checkout.Utils;

namespace PubSubWithDapr.Checkout.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderCheckoutController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<OrderCheckoutController> _logger;
    private readonly DaprClient _daprClient;

    public OrderCheckoutController(ILogger<OrderCheckoutController> logger, DaprClient daprClient)
    {
        _logger = logger;
        _daprClient = daprClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        for (int i = 1; i <= 2; i++)
        {
            var order = new Order(i);
           

            // Publish an event/message using Dapr PubSub
            await _daprClient.PublishEventAsync(AppConstants.PubSubEvent, "orders-1", order);
            Console.WriteLine("Published data: " + order);
            await _daprClient.PublishEventAsync(AppConstants.PubSubEvent, "orders-2", order);

            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();
        return forecast;
    }
}