using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Contract.Accept;

internal class AcceptContractCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public AcceptContractCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(AcceptContractCommand.IdArgument);
        await _client.AcceptContract(id, context.GetCancellationToken());

        Console.WriteLine("Accepted contract successfully.".Color(ConsoleColors.Information));
        return ExitCodes.Success;
    }
}