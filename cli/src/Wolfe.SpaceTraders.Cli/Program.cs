using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.Reflection;
using Wolfe.SpaceTraders.Cli.Commands;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection()
    .AddLogging(builder => builder
        .AddConfiguration(configuration.GetSection("Logging"))
        .AddJsonFile()
    )
    .AddInfrastructureLayer(configuration)
    .AddServiceLayer(configuration);

var handlers = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsAssignableTo(typeof(ICommandHandler)) && !t.IsAbstract).ToList();
foreach (var handler in handlers)
{
    services.AddTransient(handler, handler);
}

var provider = services.BuildServiceProvider();

var rootCommand = RootCommand.CreateCommand(provider);
var parser = new CommandLineBuilder(rootCommand)
    .UseHelp()
    .UseVersionOption()
    .UseParseErrorReporting()
    .UseExceptionHandler((ex, ctx) =>
    {
        if (ex is SpaceTradersApiException spEx)
        {
            ctx.Console.Error.WriteLine($"Error: {spEx.Message} ({spEx.ErrorCode})".Color(ConsoleColors.Error));
        }
        else
        {
            ctx.Console.Error.WriteLine($"Error: {ex.Message}");
        }
    })
    .CancelOnProcessTermination()
    .Build();

if (args.Length != 0)
{
    return await parser.InvokeAsync(args);
}

string? commandLine;
var lastResult = 0;
do
{
    Console.Write("> ");
    commandLine = Console.ReadLine();
    if (!string.IsNullOrEmpty(commandLine))
    {
        lastResult = await parser.InvokeAsync(commandLine);
    }
} while (!string.IsNullOrWhiteSpace(commandLine));
return lastResult;