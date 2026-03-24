using FluentValidation;
using MediatR;

namespace User_Registration_System.Shared.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any()) 
            {
                var context = new ValidationContext<TRequest>((TRequest)request);

                var failures = validators
                     .Select(v => v.Validate(context))
                     .SelectMany(result => result.Errors)
                     .Where(f => f != null)
                     .ToList();

                if (failures.Count != 0)
                {


                    throw new ValidationException(failures);
                }

            }

            return await next();

        }
    }
}
