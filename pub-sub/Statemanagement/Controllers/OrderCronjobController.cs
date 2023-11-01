using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Statemanagement.Models;

namespace Statemanagement.Controllers;
[Route("ordercronjob")]
[ApiController]
public class OrderCronjobController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<WeatherForecastController> _logger;

    public OrderCronjobController(
        DaprClient daprClient,
        ILogger<WeatherForecastController> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<OrderModel>> Post()
    {
        var order = new OrderModel()
        {
            Id = 1,
            Name = "order processing"
        };

        await _daprClient
            .SaveStateAsync("statestore", "order", order);

        return Ok(order);
    }


}
