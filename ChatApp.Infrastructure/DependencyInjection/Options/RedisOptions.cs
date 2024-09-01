namespace ChatApp.Infrastructure.DependencyInjection.Options
{
    public class RedisOptions
    {
        public bool Enable { get; set; }
        public string ConnectionString { get; set; }
    }
}
