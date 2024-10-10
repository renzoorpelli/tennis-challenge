using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Infrastructure.Data.Configurations.Players;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Age)
            .IsRequired();

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Wins)
            .HasDefaultValue(0);

        builder.Property(x => x.Losses)
            .HasDefaultValue(0);

        builder.HasDiscriminator<string>("Gender")
            .HasValue<MalePlayer>("Male")
            .HasValue<FemalePlayer>("Female");
    }
}