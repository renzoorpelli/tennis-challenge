using FluentValidation;
using TennisChallenge.Application.Features.Tournaments.Queries.GetTournament;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments.Queries;

public class GetTournamentTest : TournamentsBaseIntegrationTest
{
    public GetTournamentTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetTournament_WithRandomIdentifier_ShouldReturn_404ApiResponse()
    {
        // Arrange
        var query = new GetTournament(Guid.NewGuid());
        // Act
        var response = await this.GetTournamentHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Code);
    }
    [Fact]
    public async Task GetTournament_WithWellKnownIdentifier_ShouldReturn_200ApiResponse()
    {
        // Arrange
        var query = new GetTournament(new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"));
        // Act
        var response = await this.GetTournamentHandler.HandleAsync(query, CancellationToken.None);
        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Code);
    }
    
    [Fact]
    public async Task GetTournament_WithInvalidIdentifier_ShouldThrow_ValidationException()
    {
        // Arrange
        var query = new GetTournament(Guid.Empty);
        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await this.GetTournamentHandler.HandleAsync(query, CancellationToken.None));
    }
}