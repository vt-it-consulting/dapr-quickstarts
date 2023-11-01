using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Statemanagement.Models;

namespace Statemanagement.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderBindingController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<WeatherForecastController> _logger;

    public OrderBindingController(
        DaprClient daprClient,
        ILogger<WeatherForecastController> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }


    [HttpPost("/azure-order-processing-received-binding")]
    public ActionResult Post(OrderModel order)
    {
        // Handle order
        Console.WriteLine("order received: {0}: {1}", order.Id, order.Name);

        // Acknowledge message
        return Ok();
    }

    [HttpPost("/aws-order-processing-received-binding")]
    public ActionResult PostAwsBinding(OrderModel order)
    {
        // Handle order
        Console.WriteLine("order received: {0}: {1}", order.Id, order.Name);

        // Acknowledge message
        return Ok();
    }


}
