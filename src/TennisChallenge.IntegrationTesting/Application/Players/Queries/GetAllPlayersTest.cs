using TennisChallenge.Application.Features.Players.Queries.GetAll;

namespace TennisChallenge.IntegrationTesting.Application.Players.Queries;

public class GetAllPlayersTest : BasePlayerIntegrationTest
{
    public GetAllPlayersTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetAllPlayers_ShouldReturn_CollectionOfPlayers()
    {
        // Arrange
        var query = new GetAllPlayers();
        // Act
        var response = await this.GetAllPlayersHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
    }
}