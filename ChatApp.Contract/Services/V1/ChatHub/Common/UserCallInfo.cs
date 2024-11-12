

namespace ChatApp.Contract.Services.V1.ChatHub.Common
{
    public class UserCallInfo
    {
        public bool IsCalling { get; set; } = false;
        public Guid? RoomId { get; set; } = null;
        public bool IsChatPrivate { get; set; } = true;

    }
}
