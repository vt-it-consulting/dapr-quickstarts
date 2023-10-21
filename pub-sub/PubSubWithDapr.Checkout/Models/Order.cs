using System.Text.Json.Serialization;

namespace PubSubWithDapr.Checkout.Models;

public record Order([property: JsonPropertyName("orderId")]
    int OrderId);