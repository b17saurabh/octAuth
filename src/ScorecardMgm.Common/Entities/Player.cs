namespace ScorecardMgm.Common.Entities;

public class Player
{
    public string PlayerId { get; set; } = string.Empty;
    public string? PlayerName { get; set; }
    public string TeamId { get; set; } = string.Empty;

    public virtual Team? Team { get; set; }
}
