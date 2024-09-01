using FluentValidation;


namespace ChatApp.Contract.Services.V1.Identty.Validators
{
    public class LoginValidator : AbstractValidator<Query.LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}
