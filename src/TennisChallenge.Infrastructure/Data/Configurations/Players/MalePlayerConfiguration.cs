using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Infrastructure.Data.Seeds;

namespace TennisChallenge.Infrastructure.Data.Configurations.Players;

public class MalePlayerConfiguration : IEntityTypeConfiguration<MalePlayer>
{
    public void Configure(EntityTypeBuilder<MalePlayer> builder)
    {
        builder.Property(x => x.Force)
            .IsRequired()
            .HasDefaultValue(20);
        
        builder.Property(x => x.Velocity)
            .IsRequired()
            .HasDefaultValue(20);
        
        builder.HasData(DatabaseInitialSeed.GetPlayersSeed.Where(x => x.Gender == "Male"));
    }
}