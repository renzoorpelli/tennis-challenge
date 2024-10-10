using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TennisChallenge.Application.Features.Tournaments.Commands.CreateTournament;

namespace TennisChallenge.IntegrationTesting.Application.Tournaments.Commands;

public class CreateTournamentTest : TournamentsBaseIntegrationTest
{
    public CreateTournamentTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task CreateTournament_WithValidParams_ShouldReturn200ApiResponseAndBePersisted()
    {
        // Arrange
        var command = new CreateTournament(
            "ATP Open Test",
            "Male",
            new DateTime(2024, 10, 15),
            new DateTime(2024, 10, 20),
            new HashSet<Guid>()
            {
                Guid.Parse("b5e2c59e-d935-45b6-b087-1d74e4e11378"),
                Guid.Parse("4b9d627f-836e-4c43-954c-baaf1053a035"),
                Guid.Parse("8354cd01-71fa-47c2-a66f-6042ee0907ac"),
                Guid.Parse("02891735-bd59-4c05-b5bd-9e203ec8f45e"),
            });
        
        
        // Act
        await CreateTournamentHandler.HandleAsync(command, CancellationToken.None);
        var commandCreated = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Name == command.Name);
        
        // Assert
        Assert.NotNull(commandCreated);
        Assert.Equal(command.Name, commandCreated.Name);
    }
    [Fact]
    public async Task CreateTournament_WithInvalidDates_ShouldThrowAnExceptionAndNotBePersisted()
    {
        // Arrange
        var command = new CreateTournament(
            "ATP Open Test 1",
            "Male",
            new DateTime(2024, 10, 15),
            new DateTime(2020, 10, 20),
            new HashSet<Guid>()
            {
                Guid.Parse("b5e2c59e-d935-45b6-b087-1d74e4e11378"),
                Guid.Parse("4b9d627f-836e-4c43-954c-baaf1053a035"),
                Guid.Parse("8354cd01-71fa-47c2-a66f-6042ee0907ac"),
                Guid.Parse("02891735-bd59-4c05-b5bd-9e203ec8f45e"),
            });
        
        // ACT & ASSERT
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await CreateTournamentHandler.HandleAsync(command, CancellationToken.None));
        var commandCreated = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Name == command.Name);
        
        Assert.Null(commandCreated);
    }
    [Fact]
    public async Task CreateTournament_WithInvalidTournamentType_ShouldThrowAnExceptionAndNotBePersisted()
    {
        // Arrange
        var command = new CreateTournament(
            "ATP Open Test 2",
            "Mixed",
            new DateTime(2024, 10, 15),
            new DateTime(2024, 10, 20),
            new HashSet<Guid>()
            {
                Guid.Parse("b5e2c59e-d935-45b6-b087-1d74e4e11378"),
                Guid.Parse("4b9d627f-836e-4c43-954c-baaf1053a035"),
                Guid.Parse("8354cd01-71fa-47c2-a66f-6042ee0907ac"),
                Guid.Parse("02891735-bd59-4c05-b5bd-9e203ec8f45e"),
            });
        
        // ACT & ASSERT
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await CreateTournamentHandler.HandleAsync(command, CancellationToken.None));
        var commandCreated = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Name == command.Name);
        
        Assert.Null(commandCreated);
    }
    [Fact]
    public async Task CreateTournament_WithInvalidPlayerList_ShouldThrowAnExceptionAndNotBePersisted()
    {
        // Arrange
        var command = new CreateTournament(
            "ATP Open Test 3",
            "Male",
            new DateTime(2024, 10, 15),
            new DateTime(2024, 10, 20),
            new HashSet<Guid>()
            {
            });
        
        // ACT & ASSERT
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await CreateTournamentHandler.HandleAsync(command, CancellationToken.None));
        var commandCreated = await DbContext.Tournaments.FirstOrDefaultAsync(x => x.Name == command.Name);
        Assert.Null(commandCreated);
    }
}