using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities
{
    public class RoleClaimsConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
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
