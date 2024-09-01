

namespace ChatApp.Domain.Exceptions
{
    public static class RoomChatException
    {
        public class RoomChatNotFoundException : NotFoundException
        {
            public RoomChatNotFoundException(Guid roomChatId) : base($"The room chat with the id {roomChatId} was not found !")
            {
            }
        }
        public class RoomChatMustHaveAtLeastTwoMembers : BadRequestException
        {
            public RoomChatMustHaveAtLeastTwoMembers() : base("Room chat must have at least two member ")
            {
                
            }
        }
        public class GroupChatMustHaveAtLeastThreeMembers : BadRequestException
        {
            public GroupChatMustHaveAtLeastThreeMembers() : base("Group chat must have at least three member")
            {
                 
            }
        }
        public class MemberIdIsInvalid : BadRequestException
        {
            public MemberIdIsInvalid(Guid memberId) : base($"The user whose id is {memberId} does not exist")
            {
            }
        }
    }
}
