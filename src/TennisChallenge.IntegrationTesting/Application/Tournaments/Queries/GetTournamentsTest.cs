using TennisChallenge.Application.Features.Tournaments.Queries.GetTournaments;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments.Queries;

public class GetTournamentsTest : TournamentsBaseIntegrationTest
{
    public GetTournamentsTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetAllTournaments_ShouldReturn_CollectionOfTournaments()
    {
        // Arrange
        var query = new GetAllTournaments();
        // Act
        var response = await this.GetAllTournamentsHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
    }
}