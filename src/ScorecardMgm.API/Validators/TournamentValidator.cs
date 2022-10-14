using ScorecardMgm.API.Contracts;
using FluentValidation;

namespace ScoreBoardMgm.API.Validators
{
    public class TournamentValidator : AbstractValidator<Tournament>
    {
        public TournamentValidator()
        {
            RuleFor(x => x.TournamentName).NotEmpty().WithMessage("Tournament name is required");
        }
    }
}