using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using Wolfe.SpaceTraders.Cli;
using Wolfe.SpaceTraders.Cli.Commands;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Sdk;

var configuration = Configuration.CreateConfiguration();
var services = Configuration.CreateServices(configuration);

var rootCommand = RootCommand.CreateCommand(services);
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