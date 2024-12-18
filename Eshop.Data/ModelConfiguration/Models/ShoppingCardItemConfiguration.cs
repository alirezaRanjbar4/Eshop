using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class ShoppingCardItemConfiguration : IEntityTypeConfiguration<ShoppingCardItemEntity>
    {
        public void Configure(EntityTypeBuilder<ShoppingCardItemEntity> builder)
        {
            builder.ToTable("ShoppingCardItem", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Count).HasColumnType(DataTypes.Int.ToString());
            builder.Property(x => x.ProductId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
            builder.Property(x => x.CustomerId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ShoppingCardItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.ShoppingCardItems)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}