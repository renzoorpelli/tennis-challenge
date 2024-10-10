using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Infrastructure.Data.Seeds;


namespace TennisChallenge.Infrastructure.Data.Configurations.Matches;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("Matches");

        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Id)
            .IncludeProperties(new[] { "PlayerOneId", "PlayerTwoId", "TournamentId" });
        
        builder.Property(m => m.PlayerOneId)
            .IsRequired();

        builder.Property(m => m.PlayerTwoId)
            .IsRequired();

        builder.Property(m => m.TournamentId)
            .IsRequired();

        builder.Property(m => m.PlayerOnePoints)
            .IsRequired();

        builder.Property(m => m.PlayerTwoPoints)
            .IsRequired();

        builder.Property(m => m.WinnerId)
            .IsRequired(false);

        builder.Property(m => m.MatchDate)
            .IsRequired();

        builder.Property(r => r.Round);

        builder.HasOne(m => m.PlayerOne)
            .WithMany()
            .HasForeignKey(m => m.PlayerOneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.PlayerTwo)
            .WithMany()
            .HasForeignKey(m => m.PlayerTwoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Tournament)
            .WithMany(t => t.Matches)
            .HasForeignKey(m => m.TournamentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // builder.HasData(DatabaseInitialSeed.GetMatchesSeed);
    }
}