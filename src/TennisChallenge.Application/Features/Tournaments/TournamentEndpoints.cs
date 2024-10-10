using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateMatches;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateTournament;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournament;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournamentMatches;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournaments;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Application.Features.Tournaments;

[ApiController]
[Route("api/[controller]")]
public class TournamentsController : ControllerBase
{
    private readonly IQueryHandler<GetAllTournaments, ApiResponse<List<Tournament>>> _getAllTournamentsHandler;
    private readonly IQueryHandler<GetTournament, ApiResponse<Tournament>> _getTournamentHandler;
    private readonly IQueryHandler<GetTournamentMatches, ApiResponse<List<TournamentMatchDto>>> _getMatchesHandler;
    private readonly ICommandHandler<CreateTournament> _createTournamentHandler;
    private readonly ICommandHandler<GenerateMatches> _generateTournamentMatchesHandler;

    public TournamentsController(
        IQueryHandler<GetAllTournaments, ApiResponse<List<Tournament>>> getAllTournamentsHandler,
        IQueryHandler<GetTournament, ApiResponse<Tournament>> getTournamentHandler,
        IQueryHandler<GetTournamentMatches, ApiResponse<List<TournamentMatchDto>>> getMatchesHandler,
        ICommandHandler<CreateTournament> createTournamentHandler,
        ICommandHandler<GenerateMatches> generateTournamentMatchesHandler
    )
    {
        _getAllTournamentsHandler = getAllTournamentsHandler;
        _getTournamentHandler = getTournamentHandler;
        _createTournamentHandler = createTournamentHandler;
        _generateTournamentMatchesHandler = generateTournamentMatchesHandler;
        _getMatchesHandler = getMatchesHandler;
    }

    [OpenApiOperation(
        "Get all tournaments",
        "Get all available tournaments")]
    [HttpGet()]
    [ProducesResponseType<ApiResponse<List<Tournament>>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTournamentsAsync(CancellationToken cancellationToken)
        => MapApiResponse.ToActionResult(
            await _getAllTournamentsHandler.HandleAsync(new GetAllTournaments(), cancellationToken));

    [OpenApiOperation(
        "Get a tournament",
        "Get tournament full description")]
    [HttpGet("{tournamentId:guid}")]
    [ProducesResponseType<ApiResponse<List<Tournament>>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTournamentAsync(Guid tournamentId, CancellationToken cancellationToken)
        => MapApiResponse.ToActionResult(
            await _getTournamentHandler.HandleAsync(new GetTournament(tournamentId), cancellationToken));


    [HttpPost]
    [OpenApiOperation(
        "Create a Tournament",
        """
        Create a Tournament by providing the player list. 
                           The players should no be assigned to another tournament in the provided dates.
                           Once is successfully created, go to GenerateMatches to generate all the tournament flow.
        """)]
    [ProducesResponseType<ApiResponse<CreatedTournamentDTO>>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTournamentAsync(CreateTournament command,
        CancellationToken cancellationToken)
    {
        await _createTournamentHandler.HandleAsync(command, cancellationToken);

        return Created();
    }

    [HttpPut("matches/generate/{tournamentId:guid}")]
    [OpenApiOperation(
        "Generate matches for a tournament",
        "Generate a tournament matches based on the list of players of the match.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GenerateTournamentMatchesAsync(Guid tournamentId,
        CancellationToken cancellationToken)
    {
        await _generateTournamentMatchesHandler.HandleAsync(new GenerateMatches(tournamentId), cancellationToken);

        return NoContent();
    }

    [HttpGet("matches/{tournamentId:guid}")]
    [OpenApiOperation(
        "See Matches result",
        "See the matches details and it result's from the previously generated tournament")]
    [ProducesResponseType<ApiResponse<List<TournamentMatchDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMatchesDetailsAsync(Guid tournamentId,
        CancellationToken cancellationToken)
        => MapApiResponse.ToActionResult(
            await _getMatchesHandler.HandleAsync(new GetTournamentMatches(tournamentId), cancellationToken));
}