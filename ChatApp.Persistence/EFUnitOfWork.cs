using ChatApp.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public EFUnitOfWork(ApplicationDbContext context)
        {
            _context = context;   
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync();
        }
    }
}
