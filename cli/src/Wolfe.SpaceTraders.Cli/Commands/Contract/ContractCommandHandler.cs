using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract;

internal class ContractCommandHandler(IContractService service) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(ContractCommand.ContractIdArgument);

        try
        {
            var contract = await service.GetContract(id, context.GetCancellationToken())
                ?? throw new Exception($"Contract '{id}' not found.");

            ContractFormatter.WriteContract(contract);

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting contract: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}