using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models;

public class SchedulerTaskConfiguration : IEntityTypeConfiguration<SchedulerTaskEntity>
{
    public void Configure(EntityTypeBuilder<SchedulerTaskEntity> builder)
    {
        builder.ToTable("SchedulerTask", DbSchema.Model.ToString());
        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
        builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
        builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.Title).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(100).IsRequired(true);
        builder.Property(c => c.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);
        builder.Property(c => c.Priority).HasColumnType(DataTypes.Tinyint.ToString());
        builder.Property(c => c.Type).HasColumnType(DataTypes.Tinyint.ToString());
        builder.Property(c => c.RepetType).HasColumnType(DataTypes.Tinyint.ToString());
        builder.Property(c => c.Date).HasColumnType(DataTypes.Date.ToString()).IsRequired(true);
        builder.Property(c => c.Time).HasColumnType(DataTypes.Time.ToString()).IsRequired(true);
        builder.Property(c => c.ReminderDateTime).HasColumnType(DataTypes.Datetime.ToString()).IsRequired(false);
        builder.Property(c => c.RepeatCount).HasColumnType(DataTypes.Int.ToString());
        builder.Property(c => c.RelatedId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(false);
    }
}