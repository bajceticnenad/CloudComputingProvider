using FluentValidation;
using MediatR;

namespace CloudComputingProvider.Middlewares
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                var error = string.Join(Environment.NewLine,
                    failures.Select(x => $"{x.PropertyName} - {x.ErrorMessage}"));
                //throw new BadRequestException(error);
                throw new ValidationException(error);
            }

            return await next();
        }
    }
}
