

using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Persistence.Configurations
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(TableNames.Message);
            builder.ToTable(tb => tb.UseSqlOutputClause(false));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Type).HasDefaultValue(TypeMessage.String).HasConversion<int>();
            builder.Property(x => x.Status).HasDefaultValue(StatusMessage.Sent).HasConversion<int>();
        }
    }
}
