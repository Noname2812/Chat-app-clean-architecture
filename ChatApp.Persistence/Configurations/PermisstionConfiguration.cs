

using ChatApp.Domain.Entities.Identity;
using ChatApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.Configurations
{

    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(x => new { x.RoleId, x.FunctionId, x.ActionId });
        }
    }
}
