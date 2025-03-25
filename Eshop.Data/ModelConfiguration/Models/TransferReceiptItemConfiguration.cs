using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class TransferReceiptItemConfiguration : IEntityTypeConfiguration<TransferReceiptItemEntity>
    {
        public void Configure(EntityTypeBuilder<TransferReceiptItemEntity> builder)
        {
            builder.ToTable("TransferReceiptItem", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Count).HasColumnType(DataTypes.Float.ToString());
            builder.Property(x => x.TransferReceiptId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.ProductId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.EnteredWarehouseLocationId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.ExitedWarehouseLocationId).HasColumnType(DataTypes.UniqueIdentifier.ToString());

            builder
                .HasOne(x => x.TransferReceipt)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.TransferReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Product)
               .WithMany(x => x.TransferReceiptItems)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.EnteredWarehouseLocation)
               .WithMany(x => x.EnteredTransferReceiptItems)
               .HasForeignKey(x => x.EnteredWarehouseLocationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
             .HasOne(x => x.ExitedWarehouseLocation)
             .WithMany(x => x.ExitedTransferReceiptItems)
             .HasForeignKey(x => x.EnteredWarehouseLocationId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}