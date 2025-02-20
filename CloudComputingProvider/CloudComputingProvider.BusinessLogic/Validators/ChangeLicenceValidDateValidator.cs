using CloudComputingProvider.BusinessModel.Commands;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class ChangeLicenceValidDateValidator : AbstractValidator<ChangeLicenceValidDateCommand>
    {
        public ChangeLicenceValidDateValidator()
        {
            RuleFor(x => x.LicenceId)
                .GreaterThan(0)
                .WithMessage($"LicenceId must be greater than 0.");

            RuleFor(x => x.ValidToDate)
                .NotEmpty()
                .WithMessage($"Valid Date is not a valid date.");
        }
    }
}
