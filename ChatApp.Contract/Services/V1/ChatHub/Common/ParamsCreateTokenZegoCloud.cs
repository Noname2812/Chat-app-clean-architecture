namespace ChatApp.Contract.Services.V1.ChatHub.Common
{
    public class ParamsCreateTokenZegoCloud
    {
        public uint AppId { get; set; }
        public string Secret { get; set; }
        public long EffectiveTimeInSeconds { get; set; }
        public string Payload { get; set; }
    }
}
