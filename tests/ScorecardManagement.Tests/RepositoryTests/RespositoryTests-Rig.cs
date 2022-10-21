using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Repositories.Implementations;
using Xunit.Extensions.Ordering;

namespace ScorecardMgm.Tests.RepositoryTests
{
    public class RepositoryTests
    {
        private readonly ScorecardMgmContext _context;
        private readonly TournamentRepository _tournamentRepository;

        public RepositoryTests(ScorecardMgmContext context, TournamentRepository tournamentRepository)
        {
            _context = context;
            _tournamentRepository = tournamentRepository;
        }


        [Fact, Order(1)]
        public async void SaveAsync_Tournament_TournamentSaved()
        {
            // Arrange
            var tournament = new Tournament()
            {
                TournamentId = "1",
                TournamentName = "Test Tournament"
            };


            // Act
            await (_tournamentRepository.AddTournament(tournament));

            Tournament tournamentFromDb = await _tournamentRepository.GetTournament(tournament.TournamentId);


            // Assert
            Assert.Equal(tournament.TournamentId, tournamentFromDb.TournamentId);


        }
    }
}