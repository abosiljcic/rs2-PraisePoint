using FluentValidation;
using MediatR;
using Posts.Application.Exceptions;

namespace Posts.Application.Behaviors;

// Za koje zahteve ce ovaj Validator biti pozvan.
// Treba da zna koje sve validatora treba da pozove.
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            // Proveravamo da li su svi validatori zadovoljeni za neki konkretan zahtev.
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null);
            if (failures.Any())
            {
                throw new ValidationFailedException(failures);
            }
        }
        return await next();
    }
}