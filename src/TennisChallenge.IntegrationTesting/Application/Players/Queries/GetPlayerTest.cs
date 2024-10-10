using FluentValidation;
using TennisChallenge.Application.Features.Players.Queries.GetPlayer;

namespace TennisChallenge.IntegrationTesting.Application.Players.Queries;

public class GetPlayerTest : BasePlayerIntegrationTest
{
    public GetPlayerTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task GetPlayer_WithRandomIdentifier_ShouldReturn_404ApiResponse()
    {
        // Arrange
        var query = new GetPlayer(Guid.NewGuid());
        // Act
        var response = await this.GetPlayerHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Code);
    }
    [Fact]
    public async Task GetPlayer_WithWellKnownIdentifier_ShouldReturn_200ApiResponse()
    {
        // Arrange
        var query = new GetPlayer(new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"));
        // Act
        var response = await this.GetPlayerHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Code);
    }
    
    [Fact]
    public async Task GetPlayer_WithInvalidIdentifier_ShouldThrow_ValidationException()
    {
        // Arrange
        var query = new GetPlayer(Guid.Empty);
        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await this.GetPlayerHandler.HandleAsync(query, CancellationToken.None));
    }
}