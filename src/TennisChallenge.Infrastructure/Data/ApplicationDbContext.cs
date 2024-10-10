using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Infrastructure.Data;

public sealed class ApplicationDbContext 
    : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<PlayerTournament> PlayerTournaments { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}