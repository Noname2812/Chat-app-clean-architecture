using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.Friend.Command;
using static ChatApp.Contract.Services.V1.Friend.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Commands.Friend
{
    public sealed class CreateRequestAddFriendCommandHandler : CommandHandlerBase<Domain.Entities.Friend, Guid>, ICommandHandler<CreateRequestAddFriendCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private IPublisher _publisher;
        public CreateRequestAddFriendCommandHandler(IRepositoryBase<Domain.Entities.Friend, Guid> repository,
            UserManager<AppUser> userManager, IPublisher publisher)
            : base(repository)
        {
            _userManager = userManager;
            _publisher = publisher;
        }
        public async Task<Result> Handle(CreateRequestAddFriendCommand request, CancellationToken cancellationToken)
        {
            // check user exist
            var friend = await _userManager.FindByIdAsync(request.To.ToString()) ?? throw new IdentityException.UserNotFound(request.To);
            _repository.Add(new Domain.Entities.Friend
            {
                UserId = (Guid)request.From,
                FriendId = request.To,
                Status = StatusFriend.Pending,
                CreatedDate = DateTimeOffset.Now,
            });
            await _publisher.Publish(new CreatedRequestAddFriendEvent(Guid.NewGuid(), request.To), cancellationToken);
            return Result.Success();
        }
    }
}
