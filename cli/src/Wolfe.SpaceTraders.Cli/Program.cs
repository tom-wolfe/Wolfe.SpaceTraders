using Cocona;
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

app.AddCommands<AgentCommands>();
app.AddCommands<ContractCommands>();
app.AddCommands<ExplorationCommands>();
app.AddCommands<FleetCommands>();
app.AddCommands<MissionCommands>();
app.AddCommands<ShipCommands>();

app.Run();