using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;

internal class AcceptContractCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public AcceptContractCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var contractId = context.BindingContext.ParseResult.GetValueForArgument(AcceptContractCommand.ContractIdArgument);
        await _client.AcceptContract(contractId, context.GetCancellationToken());

        Console.WriteLine("Accepted contract successfully.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}