namespace ScorecardMgm.Common.Entities;

public class Tournament
{
    public string TournamentId { get; set; } = string.Empty;
    public string? TournamentName { get; set; }

    public virtual List<Match>? Matches { get; set; }

}