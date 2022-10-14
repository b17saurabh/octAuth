namespace ScorecardMgm.Common.Entities;

public class Over
{
    public string OverId { get; set; } = string.Empty;

    public string MatchId { get; set; } = string.Empty;

    public int RunCount { get; set; }

    public virtual Match? Match { get; set; }

}
