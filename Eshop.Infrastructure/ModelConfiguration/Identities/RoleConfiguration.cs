using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Role", DbSchema.Identity.ToString());

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");


        // builder.Property(x => x.TenantId).IsRequired();

        builder.Property(x => x.CreateById).IsRequired(true);
        builder.Property(x => x.ModifiedById).IsRequired(false);


        // Each Role can have many entries in the UserRole join table
        builder.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany(e => e.RoleClaims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();

    }
}
