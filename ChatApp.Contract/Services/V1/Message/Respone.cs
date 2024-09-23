namespace ChatApp.Contract.Services.V1.Message
{
    public static class Respone
    {
        public class SeenBy
        {
            public Guid UserId { get; set; }
            public string Avatar { get; set; }
            public string Name { get; set; }
            public DateTimeOffset SeenAt { get; set; }
        }
    }
}
