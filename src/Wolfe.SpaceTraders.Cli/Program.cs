using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Wolfe.SpaceTraders.Cli;
using Wolfe.SpaceTraders.Cli.Commands;

var configuration = Configuration.CreateConfiguration();
var services = Configuration.CreateServices(configuration);

var rootCommand = RootCommand.CreateCommand(services);
var parser = new CommandLineBuilder(rootCommand)
    .UseHelp()
    .UseVersionOption()
    .UseParseErrorReporting()
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
        Console.WriteLine();
    }
} while (!string.IsNullOrWhiteSpace(commandLine));
return lastResult;