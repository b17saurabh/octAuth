namespace ScorecardMgm.API.Contracts;

public class Match
{
    public string? MatchName { get; set; }
    public string WinnerTeamId { get; set; } = string.Empty;
}