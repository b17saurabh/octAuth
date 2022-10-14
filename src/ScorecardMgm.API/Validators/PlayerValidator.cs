using ScorecardMgm.API.Contracts;
using FluentValidation;

namespace ScoreBoardMgm.API.Validators;

public class PlayerValidator : AbstractValidator<Player>
{
    public PlayerValidator()
    {
        RuleFor(x => x.PlayerName).NotEmpty().WithMessage("Team name is required");
        RuleFor(x => x.TeamId).NotEmpty().WithMessage("Team id is required");
    }
}