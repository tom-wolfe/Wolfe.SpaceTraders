using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Wolfe.SpaceTraders.Cli.Commands;

namespace Wolfe.SpaceTraders.Cli;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var configuration = Configuration.CreateConfiguration();
        var services = Configuration.CreateServices(configuration);

        var rootCommand = RootCommand.CreateCommand(services);
        var parser = new CommandLineBuilder(rootCommand)
            .UseHelp()
            .UseVersionOption()
            .UseExceptionHandler()
            .CancelOnProcessTermination()
            .Build();

        if (args.Any())
        {
            return await parser.InvokeAsync(args);
        }

        string? commandLine;
        var lastResult = 0;
        do
        {
            Console.Write("Enter command: ");
            commandLine = Console.ReadLine();
            if (!string.IsNullOrEmpty(commandLine))
            {
                lastResult = await parser.InvokeAsync(commandLine);
                Console.WriteLine();
            }
        } while (!string.IsNullOrWhiteSpace(commandLine));
        return lastResult;
    }
}