using FluentValidation;

namespace ChatApp.Contract.Services.V1.RoomChat.Validators
{
    public class CreateRoomChatValidator : AbstractValidator<Command.CreateRoomChatCommand>
    {
        public CreateRoomChatValidator()
        {
            RuleFor(x => x.Members).NotEmpty();
        }
    }
}
