var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services
    .AddControllers()
    .AddDapr();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprSidekick(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapHealthChecks("/health");
    app.MapDaprMetrics();
}

app.UseCloudEvents();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
