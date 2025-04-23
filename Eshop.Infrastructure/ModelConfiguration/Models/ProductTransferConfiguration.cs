using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models
{
    public class ProductTransferConfiguration : IEntityTypeConfiguration<ProductTransferEntity>
    {
        public void Configure(EntityTypeBuilder<ProductTransferEntity> builder)
        {
            builder.ToTable("ProductTransfer", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Count).HasColumnType(DataTypes.Int.ToString());
            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Type).HasColumnType(DataTypes.Tinyint.ToString());
            builder.Property(x => x.ProductId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired();
            builder.Property(x => x.WarehouseLocationId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired();

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductTransfers)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.WarehouseLocation)
               .WithMany(x => x.ProductTransfers)
               .HasForeignKey(x => x.WarehouseLocationId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}