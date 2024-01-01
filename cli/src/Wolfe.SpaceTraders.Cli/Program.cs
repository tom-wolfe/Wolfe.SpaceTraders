using Cocona;
using Serilog;
using Wolfe.SpaceTraders.Cli;
using Wolfe.SpaceTraders.Cli.Commands;
using Wolfe.SpaceTraders.Cli.Missions;
using Wolfe.SpaceTraders.Infrastructure;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Missions;

var builder = CoconaApp.CreateBuilder();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration.GetSection("Logging"))
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog(logger: logger, dispose: true);

builder.Services
    .AddInfrastructureLayer(builder.Configuration)
    .AddServiceLayer(builder.Configuration)
    .AddSingleton<IMissionLogProvider, ConsoleMissionLogProvider>()
    .AddSingleton<IMissionLogProvider, LoggerMissionLogProvider>();

var app = builder.Build();
app.AddCommands<RootCommand>();

app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();

namespace Wolfe.SpaceTraders.Cli
{
    [HasSubCommands(typeof(AgentCommands), "agent")]
    [HasSubCommands(typeof(ContractCommands), "contract")]
    [HasSubCommands(typeof(DatabaseCommands), "database")]
    [HasSubCommands(typeof(FleetCommands), "fleet")]
    [HasSubCommands(typeof(MarketCommands), "market")]
    [HasSubCommands(typeof(MarketplaceCommands), "marketplace")]
    [HasSubCommands(typeof(MissionCommands), "mission")]
    [HasSubCommands(typeof(ShipCommands), "ship")]
    [HasSubCommands(typeof(ShipyardCommands), "shipyard")]
    [HasSubCommands(typeof(SystemCommands), "system")]
    [HasSubCommands(typeof(WaypointCommands), "waypoint")]
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class RootCommand;
}
