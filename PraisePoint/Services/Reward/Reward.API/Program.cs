using EventBus.Messages.Constants;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Reward.API.Data;
using Reward.API.EventBusConsumers;
using Reward.API.Repositories.Interfaces;
using System.Reflection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRewardContext, RewardContext>();
builder.Services.AddScoped<IPointsRepository, PointsRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// EventBus
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<AwardPointsConsumer>();
    config.AddConsumer<NewPointsConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.PointsAwardedQueue, c =>
        {
            c.ConfigureConsumer<AwardPointsConsumer>(ctx);
        });

        cfg.ReceiveEndpoint(EventBusConstants.NewPointsQueue, c =>
        {
            c.ConfigureConsumer<NewPointsConsumer>(ctx);
        });

    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
