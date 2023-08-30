
using System.Text.Json.Serialization;
using Dapr;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprSidekick(builder.Configuration);
}

var app = builder.Build();

// Dapr will send serialized event object vs. being raw CloudEvent
app.UseCloudEvents();

// needed for Dapr pub/sub routing
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapHealthChecks("/health");
    app.MapDaprMetrics();
}

// Dapr subscription in [Topic] routes orders topic to this route
app.MapPost("/orders", [Topic("orderpubsub", "orders")] (Order order) =>
{
    Console.WriteLine("Subscriber received : " + order);
    return Results.Ok(order);
});

// Dapr subscription in [Topic] routes orders topic to this route
app.MapPost("/orders-2", [Topic("orderpubsub", "orders-2")] (Order order) =>
{
    Console.WriteLine("Subscriber received (2): " + order);
    return Results.Ok(order);
});





await app.RunAsync();

public record DaprData<T>([property: JsonPropertyName("data")] T Data);


public record Order([property: JsonPropertyName("orderId")] int OrderId);

public record DaprSubscription(
  [property: JsonPropertyName("pubsubname")] string PubsubName,
  [property: JsonPropertyName("topic")] string Topic,
  [property: JsonPropertyName("route")] string Route);
