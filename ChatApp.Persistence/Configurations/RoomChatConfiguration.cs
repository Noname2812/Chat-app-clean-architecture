using ChatApp.Domain.Entities;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal class RoomChatConfiguration : IEntityTypeConfiguration<RoomChat>
    {
        public void Configure(EntityTypeBuilder<RoomChat> builder)
        {
            builder.ToTable(TableNames.RoomChat);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsGroup).HasDefaultValue(false);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            // Each RoomChat can have many Message
            builder.HasMany(r => r.Messages)
            .WithOne(m => m.RoomChat)
            .HasForeignKey(m => m.RoomChatId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
