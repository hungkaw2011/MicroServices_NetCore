﻿using Basket.API.GrpcServices;
using Basket.API.Mapper;
using Basket.API.Repositories.Interfaces;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options=>
{
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        AsyncTimeout = 30000, // Timeout cho các tương tác Redis
        SyncTimeout = 30000,  // Timeout cho các tương tác Redis đồng bộ
        EndPoints = { builder.Configuration.GetValue<string>("CacheSettings:RedisServer"), builder.Configuration.GetValue<string>("CacheSettings:RedisPort") },
    };
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>(); 
builder.Services.AddAutoMapper(typeof(BasketProfile));
// Grpc Configuration
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]));
builder.Services.AddScoped<DiscountGrpcService>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
