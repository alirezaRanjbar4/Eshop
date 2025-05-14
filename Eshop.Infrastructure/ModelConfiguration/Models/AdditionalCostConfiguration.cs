using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models
{
    public class AdditionalCostConfiguration : IEntityTypeConfiguration<AdditionalCostEntity>
    {
        public void Configure(EntityTypeBuilder<AdditionalCostEntity> builder)
        {
            builder.ToTable("AdditionalCost", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Title).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(200);
            builder.Property(x => x.Amount).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.Date).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).IsRequired(false).HasMaxLength(4000);
            builder.Property(x => x.StoreId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);


            builder
                .HasOne(x => x.Store)
                .WithMany(x => x.AdditionalCosts)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}