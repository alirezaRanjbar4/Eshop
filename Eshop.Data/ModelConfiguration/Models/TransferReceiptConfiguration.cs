using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class TransferReceiptConfiguration : IEntityTypeConfiguration<TransferReceiptEntity>
    {
        public void Configure(EntityTypeBuilder<TransferReceiptEntity> builder)
        {
            builder.ToTable("TransferReceipt", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Date).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.IsFinalized).HasColumnType(DataTypes.Bit.ToString());
            builder.Property(x => x.StoreId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Store)
                .WithMany(x => x.TransferReceipts)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}