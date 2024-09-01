

using ChatApp.Domain.Exceptions;

namespace ChatApp.Application.Exceptions;

public sealed class ValidationException : DomainException
{
    public IReadOnlyCollection<ValidationError> Errors;
    public ValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation failure", "One or more validation errors occurred")
        => Errors = errors;
}
public record ValidationError(string PropertyName, string ErrorMessage);
