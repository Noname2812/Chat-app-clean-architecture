

using ChatApp.Contract.Abstractions.Shared;
using FluentValidation;
using MediatR;

namespace ChatApp.Application.Behaviors
{
    public class ValidationPielineBehavior<TRequest, TRespone> :
        IPipelineBehavior<TRequest, TRespone> where TRequest : notnull
        where TRespone : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPielineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TRespone> Handle(TRequest request, RequestHandlerDelegate<TRespone> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            Error[] errors = _validators.Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .Select(x => new Error(x.PropertyName, x.ErrorMessage))
                .Distinct()
                .ToArray();
            if (errors.Length > 0)
            {
                return CreateValidationResult<TRespone>(errors);
            }
            return await next();
        }
        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }
            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, [errors])!;
            return (TResult)validationResult;
        }
    }
}
