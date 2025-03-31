using Eshop.Common.Enum;
using Eshop.Entity.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Identities
{
    public class Identity_RoleClaimsConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
    {
        public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
        {
            builder.ToTable("RoleClaim", DbSchema.Identity.ToString());
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoutePath).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
        }
    }
}
