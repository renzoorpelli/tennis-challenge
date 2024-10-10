using FluentValidation;
using TennisChallenge.Core.Abstractions;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Application.Features.Players.Queries.GetPlayer;

public record GetPlayer(Guid PlayerId) : IQuery<ApiResponse<Player>>;

public class GetPlayerValidator : AbstractValidator<GetPlayer>
{
    public GetPlayerValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty()
            .NotNull()
            .Must(id => Guid.TryParse(id.ToString(), out _ ))
            .NotEqual(Guid.Empty)
            .WithMessage("Provide a valid Player Identifier");
    }
}

public class GetPlayerHandler : IQueryHandler<GetPlayer, ApiResponse<Player>>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IValidator<GetPlayer> _validator;

    public GetPlayerHandler(IPlayerRepository playerRepository, IValidator<GetPlayer> validator)
    {
        _playerRepository = playerRepository;
        _validator = validator;
    }
    public async Task<ApiResponse<Player>> HandleAsync(GetPlayer query, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(query, cancellationToken);
        
        var response = await _playerRepository.GetByIdAsync(query.PlayerId, cancellationToken);
        
        return response is null 
            ? ApiResponseHelpers.SetNotFoundResponse<Player>(null)
            : ApiResponseHelpers.SetSuccessResponse<Player>(response);
    }
}