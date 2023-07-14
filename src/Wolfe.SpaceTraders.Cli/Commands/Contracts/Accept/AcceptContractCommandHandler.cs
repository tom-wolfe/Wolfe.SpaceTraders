using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Contracts.Accept;

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
        var response = await _client.AcceptContract(id, context.GetCancellationToken());

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error accepting contract: {response.StatusCode}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }

        Console.WriteLine("Accepted contract successfully.".Color(ConsoleColors.Information));
        return ExitCodes.Success;
    }
}