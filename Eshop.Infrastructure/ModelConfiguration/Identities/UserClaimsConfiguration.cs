using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities
{
    public class UserClaimsConfiguration : IEntityTypeConfiguration<UserClaimEntity>
    {
        public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
        {
            builder.ToTable("UserClaim", DbSchema.Identity.ToString());
            // builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasKey(x => x.Id);


        }
    }
}
