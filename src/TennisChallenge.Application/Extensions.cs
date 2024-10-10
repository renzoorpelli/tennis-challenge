using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Application.Features;
using TennisChallenge.Application.Features.Players.Queries.GetPlayer;

namespace TennisChallenge.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(GetPlayerValidator));
        services.AddCqrs();
        return services;
    }
}