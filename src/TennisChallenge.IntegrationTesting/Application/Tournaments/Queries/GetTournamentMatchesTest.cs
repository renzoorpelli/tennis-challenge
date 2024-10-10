using FluentValidation;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournamentMatches;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments.Queries;

public class GetTournamentMatchesTest : TournamentsBaseIntegrationTest
{
    public GetTournamentMatchesTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetTournamentMatches_WithRandomIdentifier_ShouldThrow_ValidationException()
    {
        // Arrange
        var query = new GetTournamentMatches(Guid.NewGuid());
        // Act && Assert
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await this.GetMatchesHandler.HandleAsync(query, CancellationToken.None));
    }
}