using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Infrastructure.Data.Seeds;

namespace TennisChallenge.Infrastructure.Data.Configurations.Players;

public class FemalePlayerConfiguration : IEntityTypeConfiguration<FemalePlayer>
{
    public void Configure(EntityTypeBuilder<FemalePlayer> builder)
    {
        builder.Property(x => x.ReactionTime)
            .IsRequired()
            .HasDefaultValue(20);
        
        // initial database seed
        builder.HasData(DatabaseInitialSeed.GetPlayersSeed.Where(x => x.Gender == "Female"));
    }
}