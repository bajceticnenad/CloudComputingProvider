using CloudComputingProvider.BusinessModel.Commands;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class ChangeLicencesQuantityValidator : AbstractValidator<ChangeLicencesQuantityCommand>
    {
        public ChangeLicencesQuantityValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .GreaterThan(0)
                .WithMessage($"SubscriptionId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage($"Quantity must be greater than 0.");
        }
    }
}
