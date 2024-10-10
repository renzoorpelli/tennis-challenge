using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Repositories;
using TennisChallenge.Infrastructure.Data.Decorators;
using TennisChallenge.Infrastructure.Data.Repositories;

namespace TennisChallenge.Infrastructure.Data;

internal static class Extensions
{
    internal static IServiceCollection AddPostgresDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNpgsql<ApplicationDbContext>(configuration.GetConnectionString("TennisDatabase"));
        
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddHostedService<DatabaseInitializer>();
        
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        return services;
    }
}