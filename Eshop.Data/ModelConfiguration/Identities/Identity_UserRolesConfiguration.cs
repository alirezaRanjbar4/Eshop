using Eshop.Common.Enum;
using Eshop.Entity.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Identities
{
    public class Identity_UserRolesConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("UserRole", DbSchema.Identity.ToString());
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        }
    }
}
