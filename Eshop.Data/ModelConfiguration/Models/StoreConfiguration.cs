using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class StoreConfiguration : IEntityTypeConfiguration<StoreEntity>
    {
        public void Configure(EntityTypeBuilder<StoreEntity> builder)
        {
            builder.ToTable("Store", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.EnglishName).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.Phone).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.Address).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.IsActive).HasColumnType(DataTypes.Bit.ToString());
        }
    }
}