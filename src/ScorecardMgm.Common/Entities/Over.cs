using System.ComponentModel.DataAnnotations.Schema;

namespace ScorecardMgm.Common.Entities;

public class Over
{
    public string OverId { get; set; } = string.Empty;

    public string MatchId { get; set; } = string.Empty;

    public string RunCount { get; set; }
    public string WicketCount { get; set; }


    [ForeignKey("MatchId")]
    public virtual Match? Match { get; set; }

}
