namespace ChatApp.Contract.Services.V1.Message
{
    public static class Respone
    {
        public class MessageViewer
        {
            public Guid UserId { get; set; }
            public string Avatar { get; set; }
            public string Name { get; set; }
        }
    }
}
