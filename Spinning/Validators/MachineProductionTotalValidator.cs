//using FluentValidation;

namespace Spinning.Validators
{
    public class MachineProductionTotalValidator //: AbstractValidator<SpinningMachineProductionTotal>
    {
        public MachineProductionTotalValidator()
        {
            //RuleFor(s => s.Card1Total)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .InclusiveBetween(0, 10000).WithMessage("Provide a valid total for Card 1.")
            //    .Must(BeValidCardTotal);
            //RuleFor(s => s.Card2Total).InclusiveBetween(0, 10000).WithMessage("  Provide a valid total for Card 2.");
            //RuleFor(s => s.Card3Total).InclusiveBetween(0, 10000).WithMessage("  Provide a valid total for Card 3.");
            //RuleFor(s => s.Card4Total).InclusiveBetween(0, 10000).WithMessage("  Provide a valid total for Card 4.");
            //RuleFor(s => s.RSB1Total).InclusiveBetween(0, 10000).WithMessage("  Provide a valid total for RSB 1.");
            //RuleFor(s => s.RSB2Total).InclusiveBetween(0, 10000).WithMessage("  Provide a valid total for RSB 2.");
        }

        protected bool BeValidCardTotal(decimal total)
        {
            //for extra validation
            return true;
        }
    }
}
