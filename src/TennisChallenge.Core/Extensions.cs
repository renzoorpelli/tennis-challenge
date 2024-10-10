using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Core.DomainServices;

namespace TennisChallenge.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<ITournamentService, TournamentService>();
        return services;
    }
}