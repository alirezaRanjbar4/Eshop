using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRole", DbSchema.Identity.ToString());
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        }
    }
}
