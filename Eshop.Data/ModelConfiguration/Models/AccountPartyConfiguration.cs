using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class AccountPartyConfiguration : IEntityTypeConfiguration<AccountPartyEntity>
    {
        public void Configure(EntityTypeBuilder<AccountPartyEntity> builder)
        {
            builder.ToTable("AccountParty", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.Address).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(1000);
            builder.Property(x => x.Phone).HasColumnType(DataTypes.Nvarchar.ToString()).HasMaxLength(50);
            builder.Property(x => x.CurrentCredit).HasColumnType(DataTypes.BigInt.ToString());
            builder.Property(x => x.Type).HasColumnType(DataTypes.Tinyint.ToString());
            //builder.Property(x => x.UserId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            //builder
            //    .HasOne(x => x.User)
            //    .WithOne(x => x.AccountParty)
            //    .HasForeignKey<AccountPartyEntity>(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Store)
               .WithMany(x => x.AccountParties)
               .HasForeignKey(x => x.StoreId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}