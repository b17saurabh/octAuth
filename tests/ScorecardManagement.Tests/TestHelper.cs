// namespace ScorecardManagement.Tests.TestHelper;

// public class TestHelper
// {
//     private readonly ScoreDbContext _context;
//     public TestHelper()
//     {
//         var builder = new DbContextOptionsBuilder<ScoreDbContext>();
//         builder.UseInMemoryDatabase(databaseName: "ScorecardManagement");

//         var options = builder.Options;
//         _context = new ScoreDbContext(options);
//         // Delete existing db before creating a new one

//         _context.Database.EnsureDeleted();
//         _context.Database.EnsureCreated();
//     }
// }