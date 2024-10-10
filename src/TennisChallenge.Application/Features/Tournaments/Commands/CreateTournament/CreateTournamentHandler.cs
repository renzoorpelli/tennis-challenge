using FluentValidation;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Tournaments.Commands.CreateTournament;

public record CreateTournament(
    string Name,
    string TournamentType,
    DateTime StartDate,
    DateTime EndDate,
    HashSet<Guid> PlayersIds)
    : ICommand;

public class CreateTournamentValidator
    : AbstractValidator<CreateTournament>
{
    private readonly ITournamentRepository _tournamentRepository;

    public CreateTournamentValidator(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;

        RuleFor(x => x.Name)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MaximumLength(200)
            .WithMessage("{PropertyName} cannot exceed 200 characters");

        RuleFor(x => x.TournamentType)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must(x =>
            {
                string[] allowedTypes = ["Female", "Male"];

                return allowedTypes.Contains(x);
            })
            .WithMessage("{PropertyName} allowed types are Mixed, Female and Male")
            .MaximumLength(8)
            .WithMessage("{PropertyName} cannot exceed 8 characters");

        RuleFor(x => x.StartDate)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must(BeAValidDate)
            .WithMessage("{PropertyName} must be a valid date.")
            .LessThan(x => x.EndDate)
            .WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(x => x.EndDate)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must(BeAValidDate)
            .WithMessage("{PropertyName} must be a valid date.")
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(x => x)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .MustAsync(BeUniqueTournament)
            .WithMessage("The tournament already exist!");

        RuleFor(x => x.PlayersIds)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} list cannot be empty.")
            .Must(playerIds => playerIds.All(id => Guid.TryParse(id.ToString(), out _) && id != Guid.Empty))
            .WithMessage("All {PropertyName} must be valid GUIDs.")
            .Must(playerIds => playerIds.Count is >= 2 and <= 16)
            .WithMessage("The number of players must be between 2 and 16.")
            .Must(x => IsPowerOfTwo(x.Count))
            .WithMessage("The number of players must a power of two");

        RuleFor(x => x)
            .Cascade(FluentValidation.CascadeMode.Stop)
            .MustAsync(CanPlayAsync)
            .WithMessage("Some of the players were already assigned to a tournament!");
    }

    static bool BeAValidDate(DateTime target) => target != default(DateTime);

    private async Task<bool> BeUniqueTournament(CreateTournament tournament, CancellationToken cancellationToken)
        => !await _tournamentRepository.ExistAsync(tournament.Name, tournament.TournamentType, cancellationToken);

    private bool IsPowerOfTwo(int count)
        => (count & (count - 1)) == 0;
    
    private async Task<bool> CanPlayAsync(CreateTournament tournament, CancellationToken cancellationToken)
        => await _tournamentRepository.CanPlayAsync(tournament.PlayersIds, tournament.TournamentType,
            cancellationToken);
}

internal sealed class CreateTournamentHandler : ICommandHandler<CreateTournament>
{
    private readonly ITournamentRepository _tournamentRepository;

    private readonly IValidator<CreateTournament> _validator;

    public CreateTournamentHandler(ITournamentRepository tournamentRepository,
        IValidator<CreateTournament> validator)
    {
        _tournamentRepository = tournamentRepository;
        _validator = validator;
    }

    public async Task HandleAsync(CreateTournament command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        int matchesCount = command.PlayersIds.Count - 1;

        var tournament = Tournament.Create(Guid.NewGuid(), command.StartDate, command.EndDate, command.Name,
            command.TournamentType, matchesCount);

        await _tournamentRepository.CreateAsync(tournament, cancellationToken);

        var players = command.PlayersIds.Select(p => new PlayerTournament(p, tournament.Id)).ToList();

        await _tournamentRepository.AddTournamentPlayersAsync(players, cancellationToken);
    }
}