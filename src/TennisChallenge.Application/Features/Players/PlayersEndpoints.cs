using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TennisChallenge.Application.Features.Players.Queries.GetAll;
using TennisChallenge.Application.Features.Players.Queries.GetPlayer;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.Application.Features.Players;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IQueryHandler<GetAllPlayers, ApiResponse<List<Player>>> _getAllPlayersHandler;
    private readonly IQueryHandler<GetPlayer, ApiResponse<Player>> _getPlayerHandler;

    public PlayersController(
        IQueryHandler<GetAllPlayers, ApiResponse<List<Player>>> getAllPlayersHandler,
        IQueryHandler<GetPlayer, ApiResponse<Player>> getPlayerHandler)
    {
        _getAllPlayersHandler = getAllPlayersHandler;
        _getPlayerHandler = getPlayerHandler;
    }

    [OpenApiOperation(
        "Get all players",
        "Get all available players")]
    [HttpGet()]
    [ProducesResponseType<ApiResponse<List<Player>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlayersAsync(CancellationToken cancellationToken)
        => MapApiResponse.ToActionResult(
            await _getAllPlayersHandler.HandleAsync(new GetAllPlayers(), cancellationToken));

    [OpenApiOperation(
        "Get a player",
        "Get player full description")]
    [HttpGet("{playerId:guid}")]
    [ProducesResponseType<ApiResponse<Player>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlayerAsync(Guid playerId, CancellationToken cancellationToken)
        => MapApiResponse.ToActionResult(
            await _getPlayerHandler.HandleAsync(new GetPlayer(playerId), cancellationToken));
}