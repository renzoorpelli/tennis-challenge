using FluentValidation;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Tournaments.Queries.GetTournamentMatches;

public record GetTournamentMatches(Guid TournamentId) : IQuery<ApiResponse<List<TournamentMatchDto>>>;

public class GetTournamentMatchesValidator : AbstractValidator<GetTournamentMatches>
{
    private readonly ITournamentRepository _tournamentRepository;

    public GetTournamentMatchesValidator(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;

        RuleFor(x => x.TournamentId)
            .NotEmpty()
            .NotNull()
            .Must(id => Guid.TryParse(id.ToString(), out _))
            .NotEqual(Guid.Empty)
            .WithMessage("Provide a valid Tournament Identifier")
            .MustAsync(ContainMatchesAsync)
            .WithMessage("The Tournaments doesnt contain matches!");
    }

    private async Task<bool> ContainMatchesAsync(Guid tournamentId, CancellationToken cancellationToken)
        => await _tournamentRepository.ContainMatchesAsync(tournamentId, cancellationToken);
}

public class GetTournamentMatchesHandler
    : IQueryHandler<GetTournamentMatches, ApiResponse<List<TournamentMatchDto>>>
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IValidator<GetTournamentMatches> _validator;

    public GetTournamentMatchesHandler(
        ITournamentRepository tournamentRepository,
        IValidator<GetTournamentMatches> validator)
    {
        _tournamentRepository = tournamentRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<List<TournamentMatchDto>>> HandleAsync(GetTournamentMatches query,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(query, cancellationToken);
        
        var response = await _tournamentRepository.GetTournamentMatchesAsync(query.TournamentId, cancellationToken);

        return ApiResponseHelpers.SetSuccessResponse(response);
    }
}