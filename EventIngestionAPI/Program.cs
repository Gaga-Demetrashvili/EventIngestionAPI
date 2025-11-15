using EventIngestionAPI.Endpoints;
using EventIngestionAPI.Infrastructure.Data.EntityFramework;
using EventIngestionAPI.Infrastructure.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServerDatastore(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddSingleton<IEventMapper, EventMapper>();

var app = builder.Build();

app.RegisterEventIngestionApiEndpoints();
app.RegisterMappingRuleApiEndpoints();

if (app.Configuration.GetValue<bool>("RunMigrationsOnStartup"))
{
    app.MigrateDatabase();
}

app.Run();
