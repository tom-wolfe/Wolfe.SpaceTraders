﻿using Microsoft.Extensions.Logging.Console;
using System.CommandLine.Invocation;
using System.Reflection;
using Wolfe.SpaceTraders.Logging;

namespace Wolfe.SpaceTraders;

internal static class Configuration
{
    public static IConfiguration CreateConfiguration() => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public static IServiceProvider CreateServices(IConfiguration configuration) => new ServiceCollection()
        // .AddLogger(configuration)
        .AddSpaceTradersClient(configuration)
        .AddCommandHandlers()
        .BuildServiceProvider();

    private static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration) => services
        .AddLogging(builder => builder
            .AddConfiguration(configuration.GetSection("Logging"))
            .AddConsole(options => options.FormatterName = nameof(ColoredConsoleFormatter))
            .AddConsoleFormatter<ColoredConsoleFormatter, ConsoleFormatterOptions>()
        );

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var handlers = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(ICommandHandler)) && !t.IsAbstract).ToList();
        foreach (var handler in handlers)
        {
            services.AddTransient(handler, handler);
        }
        return services;
    }
}