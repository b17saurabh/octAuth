namespace ScorecardMgm.Common.Entities;

public class Team
{
    public string TeamId { get; set; } = string.Empty;
    public string? TeamName { get; set; }

    public virtual List<Player>? Players { get; set; }

    public virtual List<Match>? Matches { get; set; }

}