using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Ordering.API.EventBusConsumer;
using Ordering.API.HostExtensions;
using Ordering.API.Mapper;
using Ordering.Application;
using Ordering.Application.Models;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //cfg.UseHealthCheck(ctx);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});
builder.Services.AddScoped<BasketCheckoutConsumer>();
builder.Services.AddAutoMapper(typeof(OrderingProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger!).Wait();
}).Run();

app.Run();
