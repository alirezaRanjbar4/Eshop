using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models
{
    public class ReceiptItemConfiguration : IEntityTypeConfiguration<ReceiptProductItemEntity>
    {
        public void Configure(EntityTypeBuilder<ReceiptProductItemEntity> builder)
        {
            builder.ToTable("ReceiptProductItem", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Count).HasColumnType(DataTypes.Float.ToString());
            builder.Property(x => x.Price).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.DiscountPrice).HasColumnType(DataTypes.BigInt.ToString()).IsRequired(false);
            builder.Property(x => x.DiscountPercent).HasColumnType(DataTypes.Int.ToString()).IsRequired(false);
            builder.Property(x => x.ValueAdded).HasColumnType(DataTypes.Int.ToString()).IsRequired(false);
            builder.Property(x => x.ReceiptId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.ProductId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.WarehouseLocationId).HasColumnType(DataTypes.UniqueIdentifier.ToString());

            builder
                .HasOne(x => x.Receipt)
                .WithMany(x => x.ProductItems)
                .HasForeignKey(x => x.ReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Product)
               .WithMany(x => x.ReceiptProductItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.WarehouseLocation)
               .WithMany(x => x.ReceiptProductItems)
               .HasForeignKey(x => x.WarehouseLocationId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}