using Microsoft.Extensions.DependencyInjection;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateMatches;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateTournament;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournament;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournamentMatches;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournaments;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments;

public class TournamentsBaseIntegrationTest : BaseIntegrationTest
{
    internal readonly IQueryHandler<GetAllTournaments, ApiResponse<List<Tournament>>> GetAllTournamentsHandler;
    internal readonly IQueryHandler<GetTournament, ApiResponse<Tournament>> GetTournamentHandler;
    internal readonly IQueryHandler<GetTournamentMatches, ApiResponse<List<TournamentMatchDto>>> GetMatchesHandler;
    internal readonly ICommandHandler<CreateTournament> CreateTournamentHandler;
    internal readonly ICommandHandler<GenerateMatches> GenerateTournamentMatchesHandler;

    public TournamentsBaseIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        GetAllTournamentsHandler = serviceScope.ServiceProvider
            .GetRequiredService<IQueryHandler<GetAllTournaments, ApiResponse<List<Tournament>>>>();
        GetTournamentHandler = serviceScope.ServiceProvider
            .GetRequiredService<IQueryHandler<GetTournament, ApiResponse<Tournament>>>();
        GetMatchesHandler = serviceScope.ServiceProvider
            .GetRequiredService<IQueryHandler<GetTournamentMatches, ApiResponse<List<TournamentMatchDto>>>>();
        CreateTournamentHandler = serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<CreateTournament>>();
        GenerateTournamentMatchesHandler =
            serviceScope.ServiceProvider.GetRequiredService<ICommandHandler<GenerateMatches>>();
    }
}