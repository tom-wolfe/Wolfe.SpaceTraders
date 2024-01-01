using Serilog;
using Wolfe.SpaceTraders;
using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Endpoints;
using Wolfe.SpaceTraders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration.GetSection("Logging"))
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog(logger: logger, dispose: true);

// Add services to the container.
builder.Services
    .AddApplicationLayer(builder.Configuration)
    .AddInfrastructureLayer(builder.Configuration)
    .AddDomainLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

// Make sure the Serilog logger is flushed on app shutdown.
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app
    .MapAgentEndpoints()
    .MapContractEndpoints()
    .MapDatabaseEndpoints()
    .MapExplorationEndpoints()
    .MapHomeEndpoints()
    .MapMissionEndpoints()
    .MapShipEndpoints()
    .MapShipyardEndpoints();

app.Run();
