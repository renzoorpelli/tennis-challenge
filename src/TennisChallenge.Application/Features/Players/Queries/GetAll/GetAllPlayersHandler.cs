using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Repositories;
using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.Application.Features.Players.Queries.GetAll;

public record GetAllPlayers : IQuery<ApiResponse<List<Player>>>;

internal sealed class GetAllPlayersHandler : IQueryHandler<GetAllPlayers, ApiResponse<List<Player>>>
{
    private readonly IPlayerRepository _playerRepository;

    public GetAllPlayersHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<ApiResponse<List<Player>>> HandleAsync(GetAllPlayers query, CancellationToken cancellationToken)
    {
        var response = (await _playerRepository.GetAllAsync(cancellationToken)).ToList();

        return ApiResponseHelpers.SetSuccessResponse(response);
    }
}