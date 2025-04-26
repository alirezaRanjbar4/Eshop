using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
        {
            builder.ToTable("RefreshToken", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Token).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000);
            builder.Property(x => x.CreatedByIp).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.RevokedByIp).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.ReplacedByToken).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000);
            builder.Property(x => x.ExpiresAt).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.CreatedAt).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.RevokedAt).HasColumnType(DataTypes.Datetime.ToString()).IsRequired(false);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
