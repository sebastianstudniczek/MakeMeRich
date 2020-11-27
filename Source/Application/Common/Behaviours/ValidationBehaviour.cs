using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MakeMeRich.Application.Common.Exceptions;

using MediatR;

namespace MakeMeRich.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)))
                    .ConfigureAwait(false);

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(failure => failure is not null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new CustomValidationException(failures);
                }
            }

            return await next().ConfigureAwait(false);
        }
    }
}
