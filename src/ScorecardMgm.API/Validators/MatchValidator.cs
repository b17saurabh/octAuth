using ScorecardMgm.API.Contracts;
using FluentValidation;

namespace ScoreBoardMgm.API.Validators;

public class MatchValidator : AbstractValidator<Match>
{
    public MatchValidator()
    {
        RuleFor(x => x.MatchName).NotEmpty().WithMessage("Team 1 id is required");
        RuleFor(x => x.WinnerTeamId).NotEmpty().WithMessage("Winner Team id is required");
    }
}