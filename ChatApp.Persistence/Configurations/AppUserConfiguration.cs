
using ChatApp.Domain.Entities.Identity;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable(TableNames.AppUsers);
            builder.HasKey(t => t.Id);
            builder.Property(u => u.IsVertify).HasDefaultValue(false);
            builder.Property(u => u.IsOnline).HasDefaultValue(false);
            builder.Property(u => u.LastOnline).HasDefaultValue(DateTimeOffset.Now);
            builder.Property(u => u.Avatar).HasDefaultValue("https://res.cloudinary.com/dvbms8xak/image/upload/v1723439983/hi9hzuznxmnq22jfdps8.jpg");
            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
