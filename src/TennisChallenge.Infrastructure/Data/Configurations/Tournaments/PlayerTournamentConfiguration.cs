using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Infrastructure.Data.Seeds;

namespace TennisChallenge.Infrastructure.Data.Configurations.Tournaments;

public class PlayerTournamentConfiguration : IEntityTypeConfiguration<PlayerTournament>
{
    public void Configure(EntityTypeBuilder<PlayerTournament> builder)
    {
        builder.HasKey(x => new { x.PlayerId, x.TournamentId });

        builder.HasData(DatabaseInitialSeed.GetPlayerTournament);
    }
}