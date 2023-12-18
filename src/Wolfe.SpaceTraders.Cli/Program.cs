using System.CommandLine.Builder;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using Wolfe.SpaceTraders.Cli;
using Wolfe.SpaceTraders.Cli.Commands;
using Wolfe.SpaceTraders.Cli.Extensions;
using HelpContext = System.CommandLine.Help.HelpContext;

var configuration = Configuration.CreateConfiguration();
var services = Configuration.CreateServices(configuration);

var rootCommand = RootCommand.CreateCommand(services);
var parser = new CommandLineBuilder(rootCommand)
    .UseHelp()
    .UseVersionOption()
    .UseExceptionHandler((ex, ctx) =>
    {
        if (ctx.ParseResult.Errors.Any())
        {
            var error = ctx.ParseResult.Errors[0].Message;
            ctx.Console.Error.WriteLine(error.Color(ConsoleColors.Error));

            var helpContext = new HelpContext(
                ctx.HelpBuilder,
                ctx.ParseResult.CommandResult.Command,
                ctx.Console.Out.CreateTextWriter(),
                ctx.ParseResult
            );
            ctx.HelpBuilder.Write(helpContext);
        }
        else
        {
            ctx.Console.Error.WriteLine(ex.ToString().Color(ConsoleColors.Error));
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
        Console.WriteLine();
    }
} while (!string.IsNullOrWhiteSpace(commandLine));
return lastResult;