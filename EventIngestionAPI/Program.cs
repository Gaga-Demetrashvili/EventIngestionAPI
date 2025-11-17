using EventIngestionAPI.Endpoints;
using EventIngestionAPI.Infrastructure.Data.EntityFramework;
using EventIngestionAPI.Infrastructure.RabbitMq;
using EventIngestionAPI.Infrastructure.Services;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServerDatastore(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddSingleton<IEventMapper, EventMapper>();
builder.Services.AddRabbitMqEventBus(builder.Configuration);

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Event Ingestion API",
        Version = "v1",
        Description = "Event Ingestion API for OnAim Tech Task",
        Contact = new OpenApiContact
        {
            Name = "Gaga Demetrashvili",
            Email = "gagademetrashvili6@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gaga-demetrashvili-7247a0222/")
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Ingestion API V1");
});

app.RegisterEventIngestionApiEndpoints();
app.RegisterMappingRuleApiEndpoints();

if (app.Configuration.GetValue<bool>("RunMigrationsOnStartup"))
{
    app.MigrateDatabase();
}

app.Run();
