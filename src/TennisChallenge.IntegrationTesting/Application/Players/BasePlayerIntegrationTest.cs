using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Application.Features.Players.Queries.GetAll;
using TennisChallenge.Application.Features.Players.Queries.GetPlayer;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.IntegrationTesting.Application.Players;

public class BasePlayerIntegrationTest : BaseIntegrationTest
{
    internal readonly IQueryHandler<GetPlayer, ApiResponse<Player>> GetPlayerHandler;
    internal readonly IQueryHandler<GetAllPlayers, ApiResponse<List<Player>>> GetAllPlayersHandler;
    
    public BasePlayerIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        GetPlayerHandler = serviceScope.ServiceProvider
            .GetRequiredService<IQueryHandler<GetPlayer, ApiResponse<Player>>>();

        GetAllPlayersHandler = serviceScope.ServiceProvider
            .GetRequiredService<IQueryHandler<GetAllPlayers, ApiResponse<List<Player>>>>();
    }
}