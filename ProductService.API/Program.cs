using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.Repositories;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using InventoryService.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Voeg Swagger services toe
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.

builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();

var app = builder.Build();

var listener = new RabbitMQListener();
await listener.StartListeningAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
