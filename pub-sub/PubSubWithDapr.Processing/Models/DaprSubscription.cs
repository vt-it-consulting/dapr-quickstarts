using System.Text.Json.Serialization;

namespace PubSubWithDapr.Processing.Models;

public record DaprData<T>([property: JsonPropertyName("data")] T Data);


public record Order([property: JsonPropertyName("orderId")] int OrderId);

public record DaprSubscription(
    [property: JsonPropertyName("pubsubname")] string PubsubName,
    [property: JsonPropertyName("topic")] string Topic,
    [property: JsonPropertyName("route")] string Route);