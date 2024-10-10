using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Infrastructure.Data.Seeds;

namespace TennisChallenge.Infrastructure.Data.Configurations.Tournaments;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasIndex(m => m.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.TournamentType)
            .IsRequired();

    builder.ToTable(t =>
            t.HasCheckConstraint("CK_Tournaments_TournamentType", "\"TournamentType\" IN ('Male', 'Female')"));

        // initial database seed
        builder.HasData(DatabaseInitialSeed.GetTournamentsSeed);
    }
}