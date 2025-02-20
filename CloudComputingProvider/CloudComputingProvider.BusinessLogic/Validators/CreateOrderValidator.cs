using CloudComputingProvider.BusinessModel.Commands;
using FluentValidation;

namespace CloudComputingProvider.BusinessLogic.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {

        }
    }
}
