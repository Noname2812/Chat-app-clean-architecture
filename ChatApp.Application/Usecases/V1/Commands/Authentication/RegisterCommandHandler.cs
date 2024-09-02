
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.Identty.Command;

namespace ChatApp.Application.Usecases.V1.Commands.Identity
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterAccountCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var isExists = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (isExists != null)
            {
                throw new IdentityException.EmailAlreadyExists(request.Email);
            }
            var user = new AppUser { Email = request.Email, Name = request.Name, UserName = request.Email    };
            var result = await _userManager.CreateAsync(user, request.Password); 
            if (!result.Succeeded)
            {
                return Result.Failure(new Error(result.Errors.First().Code, result.Errors.First().Description));
            }
            return Result.Success();
        }
    }
}