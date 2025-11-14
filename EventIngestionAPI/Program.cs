using EventIngestionAPI.Endpoints;
using EventIngestionAPI.Infrastructure.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServerDatastore(builder.Configuration);

var app = builder.Build();

app.RegisterEventIngestionApiEndpoints();
app.RegisterMappingRuleApiEndpoints();

app.Run();
