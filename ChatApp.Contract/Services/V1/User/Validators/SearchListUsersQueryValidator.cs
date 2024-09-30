using FluentValidation;


namespace ChatApp.Contract.Services.V1.User.Validators
{
    public class SearchListUsersQueryValidator : AbstractValidator<Query.SearchListUsersQuery>
    {
        public SearchListUsersQueryValidator()
        {
            RuleFor(x => x.KeySearch).NotEmpty();
        }
    }
}
