using Eshop.Common.Enum;
using Eshop.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class StorePaymentConfiguration : IEntityTypeConfiguration<StorePaymentEntity>
    {
        public void Configure(EntityTypeBuilder<StorePaymentEntity> builder)
        {
            builder.ToTable("StorePayment", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.Amount).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.Date).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.PaymentMethod).HasColumnType(DataTypes.Tinyint.ToString());
            builder.Property(x => x.StoreId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Store)
                .WithMany(x => x.StorePayments)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}