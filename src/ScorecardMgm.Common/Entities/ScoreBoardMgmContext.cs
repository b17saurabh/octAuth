using Microsoft.EntityFrameworkCore;


namespace ScorecardMgm.Common.Entities;

public class ScorecardMgmContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // if (!optionsBuilder.IsConfigured)
        // {
        //     IConfigurationRoot configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //     var connectionString = configuration.GetConnectionString("express");
        //     optionsBuilder.UseSqlServer(connectionString);
        // }
    }

    public ScorecardMgmContext(DbContextOptions<ScorecardMgmContext> options) : base(options) { }
    public DbSet<Player>? Players { get; set; }
    public DbSet<Team>? Teams { get; set; }
    public DbSet<Match>? Matches { get; set; }
    public DbSet<Over>? Overs { get; set; }
    public DbSet<Tournament>? Tournaments { get; set; }
    public DbSet<User>? Users { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Match>()
            .HasKey(c => new { c.MatchId });

        modelBuilder.Entity<Over>()
            .HasKey(c => new { c.OverId });

        modelBuilder.Entity<Player>()
            .HasKey(c => new { c.PlayerId });

        modelBuilder.Entity<Team>()
            .HasKey(c => new { c.TeamId });

        modelBuilder.Entity<Tournament>()
            .HasKey(c => new { c.TournamentId });

        modelBuilder.Entity<User>()
            .HasKey(c => new { c.id });

        base.OnModelCreating(modelBuilder);
    }


}