using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class ServicePriceConfiguration : IEntityTypeConfiguration<ServicePriceEntity>
    {
        public void Configure(EntityTypeBuilder<ServicePriceEntity> builder)
        {
            builder.ToTable("ServicePrice", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Price).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.ExpiryDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ServiceId).HasColumnType(DataTypes.UniqueIdentifier.ToString());

            builder
                .HasOne(x => x.Service)
                .WithMany(x => x.ServicePrices)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}