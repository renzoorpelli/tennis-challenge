using FluentValidation;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Tournaments.Queries.GetTournament;

public record GetTournament(Guid TournamentId): IQuery<ApiResponse<Tournament>>;

public class GetTournamentValidator : AbstractValidator<GetTournament>
{
    public GetTournamentValidator()
    {
        RuleFor(x => x.TournamentId)
            .NotEmpty()
            .NotNull()
            .Must(id => Guid.TryParse(id.ToString(), out _ ))
            .NotEqual(Guid.Empty)
            .WithMessage("Provide a valid Tournament Identifier");
    }
}

public class GetTournamentHandler : IQueryHandler<GetTournament, ApiResponse<Tournament>>
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IValidator<GetTournament> _validator;

    public GetTournamentHandler(ITournamentRepository tournamentRepository, IValidator<GetTournament> validator)
    {
        _tournamentRepository = tournamentRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<Tournament>> HandleAsync(GetTournament query, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(query, cancellationToken);
        
        var response = (await _tournamentRepository.GetByIdAsync(query.TournamentId, cancellationToken));

        return response is null 
            ? ApiResponseHelpers.SetNotFoundResponse<Tournament>() 
            : ApiResponseHelpers.SetSuccessResponse(response);
    }
}