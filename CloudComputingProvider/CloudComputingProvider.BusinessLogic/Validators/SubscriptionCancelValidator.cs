using CloudComputingProvider.BusinessModel.Commands;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class SubscriptionCancelValidator : AbstractValidator<SubscriptionCancelCommand>
    {
        public SubscriptionCancelValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .GreaterThan(0)
                .WithMessage($"SubscriptionId must be greater than 0.");
        }
    }
}
