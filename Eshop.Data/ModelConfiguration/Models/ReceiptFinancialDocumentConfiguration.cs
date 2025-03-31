using Eshop.Common.Enum;
using Eshop.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Models
{
    public class ReceiptFinancialDocumentConfiguration : IEntityTypeConfiguration<ReceiptFinancialDocumentEntity>
    {
        public void Configure(EntityTypeBuilder<ReceiptFinancialDocumentEntity> builder)
        {
            builder.ToTable("ReceiptFinancialDocument", DbSchema.Model.ToString());
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.Property(x => x.CreateDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.Property(x => x.ModifiedDate).HasColumnType(DataTypes.Datetime.ToString());
            builder.HasOne(x => x.CreateBy).WithMany().HasForeignKey(x => x.CreateById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ModifiedBy).WithMany().HasForeignKey(x => x.ModifiedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ReceiptId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);
            builder.Property(x => x.FinancialDocumentId).HasColumnType(DataTypes.UniqueIdentifier.ToString()).IsRequired(true);

            builder
                .HasOne(x => x.Receipt)
                .WithMany(x => x.ReceiptFinancialDocuments)
                .HasForeignKey(x => x.ReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.FinancialDocument)
                .WithMany(x => x.ReceiptFinancialDocuments)
                .HasForeignKey(x => x.FinancialDocumentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}