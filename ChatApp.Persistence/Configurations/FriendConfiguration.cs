

using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable(TableNames.Friend);
            builder.Ignore(f => f.Id);
            builder.HasKey(x => new { x.UserId, x.FriendId });
            builder.Property(x => x.Status).HasDefaultValue(StatusFriend.Pending).HasConversion<int>();

            builder.HasOne(x => x.UserFriend)
                .WithMany(x => x.FriendOf)
                .HasForeignKey(x => x.FriendId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserOf)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
