using static ChatApp.Contract.Services.V1.Message.Respone;
using System.Text.Json;

namespace ChatApp.Contract.Helpers
{
    public static class FunctionHelper
    {
        public static List<MessageViewer>? DeserializeSeenBy(string seenByJson)
        {
            if (string.IsNullOrEmpty(seenByJson))
            {
                return new List<MessageViewer>();
            }
            return JsonSerializer.Deserialize<List<MessageViewer>>(seenByJson);
        }
    }
}
