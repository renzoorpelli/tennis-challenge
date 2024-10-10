using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Core.Abstractions;

namespace TennisChallenge.Application.Features;

internal static class Extensions
{
    internal static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        var currentLayerAssembly = Assembly.GetExecutingAssembly();
        
        services.Scan(s => s.FromAssemblies(currentLayerAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(currentLayerAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}