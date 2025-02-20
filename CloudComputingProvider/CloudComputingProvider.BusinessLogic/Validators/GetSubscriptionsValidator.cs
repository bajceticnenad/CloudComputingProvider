using CloudComputingProvider.BusinessModel.Queries;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class GetSubscriptionsValidator : AbstractValidator<GetSubscriptionsQuery>
    {
        public GetSubscriptionsValidator()
        {
            RuleFor(x => x.CustomerAccountId)
                .GreaterThan(0)
                .WithMessage($"CustomerAccountId must be greater than 0");
        }
    }
}
