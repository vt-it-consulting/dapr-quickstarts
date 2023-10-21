using Dapr;
using Microsoft.AspNetCore.Mvc;
using PubSubWithDapr.Processing.Models;

namespace PubSubWithDapr.Processing.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderProcessingController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<OrderProcessingController> _logger;

    public OrderProcessingController(ILogger<OrderProcessingController> logger)
    {
        _logger = logger;
    }

    [Topic("orderpubsub", "orders-1")]
    [HttpPost("/orders-1")]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        Console.WriteLine("Subscriber received : " + order);
        await Task.Delay(1_000);
        return Ok(order);
    }
    
    [Topic("orderpubsub", "orders-2")]
    [HttpPost("/orders-2")]
    public async Task<ActionResult<Order>> CreateOrderEndpoint2(Order order)
    {
        Console.WriteLine("Subscriber received : " + order);
        await Task.Delay(1_000);
        return Ok(order);
    }
    
}