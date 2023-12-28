using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;

internal class AcceptContractCommandHandler(IContractService contractService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var contractId = context.BindingContext.ParseResult.GetValueForArgument(AcceptContractCommand.ContractIdArgument);
        var contract = await contractService.GetContract(contractId, context.GetCancellationToken())
            ?? throw new Exception($"Contract {contractId} could not be found.");

        await contract.Accept(context.GetCancellationToken());

        Console.WriteLine("Accepted contract successfully.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}