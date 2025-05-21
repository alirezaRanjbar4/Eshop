using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Models;

public class SchedulerTaskPersonConfiguration : IEntityTypeConfiguration<SchedulerTaskVendorEntity>
{
    public void Configure(EntityTypeBuilder<SchedulerTaskVendorEntity> builder)
    {
        builder.ToTable("SchedulerTaskVendor", DbSchema.Model.ToString());
        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
        builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
        builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.IsDone).HasColumnType(DataTypes.Bit.ToString());
        builder.Property(c => c.SchedulerTaskId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
        builder.Property(c => c.VendorId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
        builder.Property(c => c.Date).HasColumnType(DataTypes.Date.ToString()).IsRequired(true);
        builder.Property(c => c.Time).HasColumnType(DataTypes.Time.ToString()).IsRequired(true);
        builder.Property(c => c.ReminderDateTime).HasColumnType(DataTypes.Datetime.ToString()).IsRequired(false);
        builder.Property(c => c.Description).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(4000).IsRequired(false);

        builder
           .HasOne(x => x.SchedulerTask)
           .WithMany(x => x.AssignedTo)
           .HasForeignKey(x => x.SchedulerTaskId)
           .OnDelete(DeleteBehavior.Restrict);

        builder
           .HasOne(x => x.Vendor)
           .WithMany(x => x.SchedulerTaskVendors)
           .HasForeignKey(x => x.VendorId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}