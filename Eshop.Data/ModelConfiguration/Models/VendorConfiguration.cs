using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class VendorConfiguration : IEntityTypeConfiguration<VendorEntity>
    {
        public void Configure(EntityTypeBuilder<VendorEntity> builder)
        {
            builder.ToTable("Vendor", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.UserId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
            builder.Property(x => x.StoreId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Vendor)
                .HasForeignKey<VendorEntity>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Store)
                .WithMany(x => x.Vendors)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}