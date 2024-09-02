using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.User.Command;
using static ChatApp.Contract.Services.V1.User.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Commands.User
{
    public sealed class UpdateUserCommandHandler : ICommandHandler<UpadteUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublisher _publisher;
        public UpdateUserCommandHandler(UserManager<AppUser> userManager, IPublisher publisher)
        {
            _userManager = userManager;
            _publisher = publisher;
        }
        public async Task<Result> Handle(UpadteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                throw new IdentityException.UserNotFound(request.UserId);
            }
            if (request.NewPassword != null && request.CurrentPassword != null)
            {
                var isSuccess = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                return isSuccess.Succeeded
                    ? Result.Success()
                    : Result.Failure(new Error(isSuccess.Errors.First().Code, isSuccess.Errors.First().Description));
            }
            user.Name = request.Name ?? user.Name;
            user.Avatar = request.Avatar ?? user.Avatar;
            user.DayOfBirth = request.DayOfBirth ?? user.DayOfBirth;
            var result = await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(request.Name))
            {
                await _publisher.Publish(new UpdatedNameUserEvent(Guid.NewGuid(), user.Id, request.Name),cancellationToken);
            }
            return result.Succeeded ? Result.Success() : Result.Failure(new Error(result.Errors.First().Code, result.Errors.First().Description));
        }
    }
}
