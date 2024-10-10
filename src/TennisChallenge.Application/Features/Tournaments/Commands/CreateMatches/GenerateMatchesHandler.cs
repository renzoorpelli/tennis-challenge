using FluentValidation;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.DomainServices;
using TennisChallenge.Core.DTO.Player;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Tournaments.Commands.CreateMatches;

public record GenerateMatches(Guid TournamentId) : ICommand;

public class GenerateMatchesValidator : AbstractValidator<GenerateMatches>
{
    private readonly ITournamentRepository _tournamentRepository;

    public GenerateMatchesValidator(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
        
        RuleFor(x => x.TournamentId)
            .NotEmpty()
            .NotNull()
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .NotEqual(Guid.Empty)
            .WithMessage("Provide a valid Tournament Identifier")
            .MustAsync(ContainMatchesAsync)
            .WithMessage("The Tournaments contains matches!");
    }

    private async Task<bool> ContainMatchesAsync(Guid tournamentId, CancellationToken cancellationToken)
        => !await _tournamentRepository.ContainMatchesAsync(tournamentId, cancellationToken);
}

public class GenerateMatchesHandler : ICommandHandler<GenerateMatches>
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly IMatchService _matchService;
    private readonly ITournamentService _tournamentService;
    private readonly IValidator<GenerateMatches> _validator;

    public GenerateMatchesHandler(
        ITournamentRepository tournamentRepository,
        IMatchRepository matchRepository,
        IMatchService matchService,
        ITournamentService tournamentService,
        IValidator<GenerateMatches> validator)
    {
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
        _validator = validator;
        _matchService = matchService;
        _tournamentService = tournamentService;
    }

    public async Task HandleAsync(GenerateMatches command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var tournament = await _tournamentRepository.GetByIdAsync(command.TournamentId, cancellationToken);

        List<Player> tournamentPlayers =
            await _tournamentRepository.GetTournamentPlayersAsync(command.TournamentId, cancellationToken);

        if (_tournamentService.TryValidateTournament(tournament, tournamentPlayers))
        {
            var matches = _matchService.GenerateMatchesWithResults(tournamentPlayers, tournament!).ToList();
            
            var tournamentWinnerId = matches.Max(m => m.WinnerId);
            
            await _matchRepository.CreateRangeAsync(matches, cancellationToken);

            tournament!.WinnerId = tournamentWinnerId;

            _tournamentRepository.Update(tournament);
        }
    }
}