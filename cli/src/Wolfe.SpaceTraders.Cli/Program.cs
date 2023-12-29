using Cocona;
using Wolfe.SpaceTraders.Cli;
using Wolfe.SpaceTraders.Cli.Commands;
using Wolfe.SpaceTraders.Infrastructure;
using Wolfe.SpaceTraders.Service;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddLogging(b => b
    .ClearProviders()
    .AddJsonFile()
);

builder.Services
    .AddInfrastructureLayer(builder.Configuration)
    .AddServiceLayer(builder.Configuration);

var app = builder.Build();

app.AddCommands<RootCommand>();

app.Run();

namespace Wolfe.SpaceTraders.Cli
{
    [HasSubCommands(typeof(AgentCommands), "agent")]
    [HasSubCommands(typeof(ContractCommands), "contract")]
    [HasSubCommands(typeof(FleetCommands), "fleet")]
    [HasSubCommands(typeof(MarketplaceCommands), "marketplace")]
    [HasSubCommands(typeof(MissionCommands2), "mission")]
    [HasSubCommands(typeof(ShipCommands), "ship")]
    [HasSubCommands(typeof(ShipyardCommands), "shipyard")]
    [HasSubCommands(typeof(SystemCommands), "system")]
    [HasSubCommands(typeof(WaypointCommands), "waypoint")]
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class RootCommand;
}
