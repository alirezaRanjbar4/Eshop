using Eshop.Common.Enum;
using Eshop.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class DemoRequestConfiguration : IEntityTypeConfiguration<DemoRequestEntity>
    {
        public void Configure(EntityTypeBuilder<DemoRequestEntity> builder)
        {
            builder.ToTable("DemoRequest", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            //builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            //builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            //builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.StoreName).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Address).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Phone).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Phone).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.WorkBranch).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.AdminDescription).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
            builder.Property(x => x.IsAnswered).HasColumnType(DataTypes.Bit.ToString()).HasDefaultValue(false);
        }
    }
}