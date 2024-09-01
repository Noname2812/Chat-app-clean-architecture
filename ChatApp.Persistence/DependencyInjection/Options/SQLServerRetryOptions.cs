

using System.ComponentModel.DataAnnotations;

namespace ChatApp.Persistence.DependencyInjection.Options
{
    public class SQLServerRetryOptions
    {
        [Required, Range(5,20)] public int MaxRetryCount { get; init; }
        [Required, Timestamp] public TimeSpan MaxRetryDelay { get; init; }
        public int[]? ErrorNumbersToAdd { get; init; }
    }
}
