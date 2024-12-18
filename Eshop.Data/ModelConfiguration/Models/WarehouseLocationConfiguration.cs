using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class WarehouseLocationConfiguration : IEntityTypeConfiguration<WarehouseLocationEntity>
    {
        public void Configure(EntityTypeBuilder<WarehouseLocationEntity> builder)
        {
            builder.ToTable("WarehouseLocation", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.LocationNumber).HasColumnType(DataTypes.Int.ToString());
            builder.Property(x => x.WarehouseId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Warehouse)
                .WithMany(x => x.WarehouseLocations)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}