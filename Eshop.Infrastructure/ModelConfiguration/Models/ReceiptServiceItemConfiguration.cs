using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models
{
    public class ReceiptServiceItemConfiguration : IEntityTypeConfiguration<ReceiptServiceItemEntity>
    {
        public void Configure(EntityTypeBuilder<ReceiptServiceItemEntity> builder)
        {
            builder.ToTable("ReceiptServiceItem", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Count).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.Price).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.DiscountPrice).HasColumnType(DataTypes.BigInt.ToString()).IsRequired(false);
            builder.Property(x => x.DiscountPercent).HasColumnType(DataTypes.Int.ToString()).IsRequired(false);
            builder.Property(x => x.ValueAddedPercent).HasColumnType(DataTypes.Int.ToString()).IsRequired(false);
            builder.Property(x => x.ReceiptId).HasColumnType(DataTypes.UniqueIdentifier.ToString());
            builder.Property(x => x.ServiceId).HasColumnType(DataTypes.UniqueIdentifier.ToString());

            builder
                .HasOne(x => x.Receipt)
                .WithMany(x => x.ServiceItems)
                .HasForeignKey(x => x.ReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Service)
               .WithMany(x => x.ReceiptServiceItems)
               .HasForeignKey(x => x.ServiceId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}