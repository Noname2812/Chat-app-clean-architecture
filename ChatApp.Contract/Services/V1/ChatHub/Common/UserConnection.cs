namespace ChatApp.Contract.Services.V1.ChatHub.Common
{
    public class UserConnection
    {
        public string ConnectionId { get; set; }
        public bool IsCalling { get; set; } = false;
        public Guid? RoomIdCalling { get; set; } = null;
        public UserCallInfo? CallInfo { get; set; }
    }

}
