

namespace ChatApp.Domain.Exceptions
{
    public abstract class UnprocessableEntityException : DomainException
    {
        protected UnprocessableEntityException(string message) : base("Unprocessable Entity", message)
        {
        }
    }
}
