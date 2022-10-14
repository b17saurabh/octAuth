namespace ScorecardMgm.API.Contracts;

public class Match
{
    public string? MatchName { get; set; }
    public string TournamentId { get; set; } = string.Empty;

    public string WinnerTeamId { get; set; } = string.Empty;
}