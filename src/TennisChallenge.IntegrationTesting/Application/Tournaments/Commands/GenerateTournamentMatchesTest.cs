using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateMatches;
using TennisChallenge.Core.Exceptions;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments.Commands;

public class GenerateTournamentMatchesTest : TournamentsBaseIntegrationTest
{
    public GenerateTournamentMatchesTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task
        GenerateTournamentMatches_WithValidIdentifier_ShouldGenerateTournamentMatchesForTargetTournament_Return200ApiResponseAndBePersisted()
    {
        var command = new GenerateMatches(Guid.Parse("c3d4097b-d01e-4e28-aa64-56233b158d83"));

        await GenerateTournamentMatchesHandler.HandleAsync(command, CancellationToken.None);

        var commandCreated = await DbContext.PlayerTournaments.Where(x => x.TournamentId == command.TournamentId)
            .ToListAsync();

        Assert.NotNull(commandCreated);
        Assert.NotEmpty(commandCreated);
    }
    [Fact]
    public async Task GenerateTournamentMatches_WithInValidIdentifier_ShouldThrowAnException_AndNotBePersisted()
    {
        var command = new GenerateMatches(Guid.NewGuid());

        await Assert.ThrowsAsync<TournamentNotFoundException>(async () =>
            await GenerateTournamentMatchesHandler.HandleAsync(command, CancellationToken.None));

        var commandCreated = await DbContext.PlayerTournaments.Where(x => x.TournamentId == command.TournamentId)
            .ToListAsync();

        Assert.Empty(commandCreated);
    }
    [Fact]
    public async Task GenerateTournamentMatches_WithRandomIdentifier_ShouldThrowAnException_AndNotBePersisted()
    {
        var command = new GenerateMatches(Guid.Empty);

        await Assert.ThrowsAsync<ValidationException>(async () =>
            await GenerateTournamentMatchesHandler.HandleAsync(command, CancellationToken.None));

        var commandCreated = await DbContext.PlayerTournaments.Where(x => x.TournamentId == command.TournamentId)
            .ToListAsync();

        Assert.Empty(commandCreated);
    }
}