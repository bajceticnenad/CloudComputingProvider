using CloudComputingProvider.BusinessModel.Queries;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class GetCustomerAccountsQueryValidator : AbstractValidator<GetCustomerAccountsQuery>
    {
        public GetCustomerAccountsQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage($"CustomerId must be greater than 0");
        }
    }
}
