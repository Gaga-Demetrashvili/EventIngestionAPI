using EventIngestionAPI.Endpoints;
using EventIngestionAPI.Infrastructure.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddSqlServerDatastore(builder.Configuration);

app.RegisterEndpoints();

app.Run();
