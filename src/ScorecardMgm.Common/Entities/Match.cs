using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScorecardMgm.Common.Entities;

public class Match
{
    public string MatchId { get; set; } = string.Empty;
    public string MatchName { get; set; } = string.Empty;
    public string TournamentId { get; set; } = string.Empty;

    public string WinnerTeamId { get; set; } = string.Empty;

    // public virtual List<Over> Overs { get; set; }


    [ForeignKey(nameof(WinnerTeamId))]
    public virtual Team? Team { get; set; }

    [ForeignKey(nameof(TournamentId))]
    public virtual Tournament? Tournament { get; set; }
}
