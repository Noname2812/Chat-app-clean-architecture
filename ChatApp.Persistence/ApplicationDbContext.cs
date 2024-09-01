

using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Action = ChatApp.Domain.Entities.Identity.Action;
using Function = ChatApp.Domain.Entities.Identity.Function;
namespace ChatApp.Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public DbSet<AppUser> AppUses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<ActionInFunction> ActionInFunctions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoomChat> RoomChats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    }
}
