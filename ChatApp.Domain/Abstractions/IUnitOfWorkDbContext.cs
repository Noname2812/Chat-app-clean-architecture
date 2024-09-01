

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Domain.Abstractions
{
    //mutiple db
    public interface IUnitOfWorkDbContext<TContext> : IAsyncDisposable where TContext : DbContext
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        TContext GetDbContext();
    }
}
