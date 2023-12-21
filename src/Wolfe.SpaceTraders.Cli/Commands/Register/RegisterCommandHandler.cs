﻿using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Requests;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal class RegisterCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;
    private readonly ITokenWriter _token;

    public RegisterCommandHandler(ISpaceTradersClient client, ITokenWriter token)
    {
        _client = client;
        _token = token;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var symbol = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.SymbolArgument);
        var faction = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.FactionOption);
        var email = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.EmailOption);

        var request = new RegisterRequest
        {
            Symbol = symbol,
            Faction = faction ?? FactionSymbol.Cosmic, // Default faction.
            Email = email
        };
        var response = await _client.Register(request, context.GetCancellationToken());
        await _token.Write(response.Token, context.GetCancellationToken());

        Console.WriteLine($"Welcome, {response.Agent.Symbol}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}