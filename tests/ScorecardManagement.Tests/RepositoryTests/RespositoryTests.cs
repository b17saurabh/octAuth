using ScorecardMgm.Common.Entities;

namespace ScorecardMgm.Tests.RepositoryTests
{
    public class RepositoryTests
    {
        private readonly ScorecardMgmContext _context;

        public RepositoryTests(ScorecardMgmContext context)
        {
            _context = context;
        }

        public void SaveAsync_Tournament_TournamentSaved()
        {
            // Arrange
            var tournament = new Tournament();
            // var repository = new DbContextOptions<ScorecardMgmContext>;

            // Act
            _context.Tournaments.AddAsync(tournament);
            _context.SaveChangesAsync();

            // Assert
            // Assert.True(tournament.Id > 0);
        }
    }
}