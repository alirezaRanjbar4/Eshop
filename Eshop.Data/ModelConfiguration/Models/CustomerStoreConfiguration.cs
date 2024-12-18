using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class CustomerStoreConfiguration : IEntityTypeConfiguration<CustomerStoreEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerStoreEntity> builder)
        {
            builder.ToTable("CustomerStore", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CustomerId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
            builder.Property(x => x.StoreId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerStores)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Store)
               .WithMany(x => x.CustomerStores)
               .HasForeignKey(x => x.StoreId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}