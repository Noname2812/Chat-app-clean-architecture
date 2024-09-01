

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Domain.Abstractions
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        DbContext GetDbContext();
    }
}
