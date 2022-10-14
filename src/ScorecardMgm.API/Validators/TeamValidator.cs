using ScorecardMgm.API.Contracts;
using FluentValidation;

namespace ScoreBoardMgm.API.Validators;

public class TeamValidator : AbstractValidator<Team>
{
    public TeamValidator()
    {
        RuleFor(x => x.TeamName).NotEmpty().WithMessage("Team name is required");
    }
}