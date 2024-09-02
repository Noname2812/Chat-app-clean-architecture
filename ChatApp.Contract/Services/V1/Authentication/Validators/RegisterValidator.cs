

using FluentValidation;

namespace ChatApp.Contract.Services.V1.Identty.Validators
{
    public class RegisterValidator : AbstractValidator<Command.RegisterAccountCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
