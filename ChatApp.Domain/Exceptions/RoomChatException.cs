

namespace ChatApp.Domain.Exceptions
{
    public static class RoomChatException
    {
        public class RoomChatUnprocessableEntityException :UnprocessableEntityException
        {
            public RoomChatUnprocessableEntityException() : base($"Room id invalid !")
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
    }
}
