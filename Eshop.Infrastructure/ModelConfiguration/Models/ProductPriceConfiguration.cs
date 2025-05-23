﻿using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPriceEntity>
    {
        public void Configure(EntityTypeBuilder<ProductPriceEntity> builder)
        {
            builder.ToTable("ProductPrice", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Price).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.ExpiryDate).HasColumnType(DataTypes.Datetime.ToString()).IsRequired(false);
            builder.Property(x => x.ProductId).HasColumnType(DataTypes.UniqueIdentifier.ToString());

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductPrices)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}