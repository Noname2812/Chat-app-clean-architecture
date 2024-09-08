using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Enums;
using MediatR;
using static ChatApp.Contract.Services.V1.Friend.Command;

namespace ChatApp.Application.Usecases.V1.Commands.Friend
{
    public sealed class UpdateStatusFriendCommandHandler : CommandHandlerBase<Domain.Entities.Friend, Guid>, ICommandHandler<UpdateStatusFriendCommand>
    {
        private readonly IPublisher _publisher;
        public UpdateStatusFriendCommandHandler(IRepositoryBase<Domain.Entities.Friend, Guid> repository, IPublisher publisher)
            : base(repository)
        {
            _publisher = publisher;
        }
        public async Task<Result> Handle(UpdateStatusFriendCommand request, CancellationToken cancellationToken)
        {
            // check exists
            var friend = new Domain.Entities.Friend();
            if (request.Status == StatusFriend.Accepted)
            {
                // only user was reviced request add friend who can accepted
                friend = await _repository.FindSingleAsync(x => x.UserId == request.To && x.FriendId == request.From);
            }
            else
            {
                friend = await _repository.FindSingleAsync(x => x.UserId == request.From && x.FriendId == request.To
                || x.UserId == request.To && x.FriendId == request.From);
            }
            if (friend == null)
            {
                // throw exception not found friendship
                throw new Exception();
            }
            friend.Status = request.Status;
            _repository.Update(friend);
            return Result.Success();
        }
    }
}
