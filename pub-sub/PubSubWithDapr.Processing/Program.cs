var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddDaprClient();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprSidekick(builder.Configuration);
}

var app = builder.Build();

// needed for Dapr pub/sub routing
app.MapSubscribeHandler();
// Dapr will send serialized event object vs. being raw CloudEvent
app.UseCloudEvents();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapHealthChecks("/health");
    app.MapDaprMetrics();
}

// https://github.com/dapr/dotnet-sdk/issues/487

app.UseAuthorization();

app.MapControllers();

app.Run();