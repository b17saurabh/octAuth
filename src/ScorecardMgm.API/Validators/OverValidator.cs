using ScorecardMgm.API.Contracts;
using FluentValidation;

namespace ScoreBoardMgm.API.Validators;

public class OverValidator : AbstractValidator<Over>
{
    public OverValidator()
    {
        RuleFor(x => x.RunCount).NotEmpty();
        RuleFor(x => x.WicketCount).NotEmpty();
    }
}