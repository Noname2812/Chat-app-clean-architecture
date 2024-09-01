

using ChatApp.Domain.Entities;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal class ConversionParticipantConfiguration : IEntityTypeConfiguration<ConversationParticipant>
    {
        public void Configure(EntityTypeBuilder<ConversationParticipant> builder)
        {
            builder.ToTable(TableNames.ConvenstionParticipant);
            builder.Ignore(cp => cp.Id);
            builder.HasKey(x => new { x.UserId, x.RoomChatId });
            builder.Property(x => x.NickName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTimeOffset.Now);
            builder.HasOne(cp => cp.AppUser)
               .WithMany(au => au.Conversations)
               .HasForeignKey(cp => cp.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Each RoomChat can have many Users
            builder.HasOne(x => x.RoomChat)
                .WithMany(r => r.ConversationParticipants)
                .HasForeignKey(x => x.RoomChatId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); // Allow 
        }
    }
}
