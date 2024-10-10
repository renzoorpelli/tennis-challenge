using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Tournaments.Queries.GetTournaments;

public record GetAllTournaments : IQuery<ApiResponse<List<Tournament>>>;

public class GetAllTournamentsHandler : IQueryHandler<GetAllTournaments, ApiResponse<List<Tournament>>>
{
    private readonly ITournamentRepository _tournamentRepository;

    public GetAllTournamentsHandler(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<ApiResponse<List<Tournament>>> HandleAsync(GetAllTournaments query, CancellationToken cancellationToken)
    {
        var response = (await _tournamentRepository.GetAllAsync(cancellationToken)).ToList();

        return ApiResponseHelpers.SetSuccessResponse(response);
    }
}