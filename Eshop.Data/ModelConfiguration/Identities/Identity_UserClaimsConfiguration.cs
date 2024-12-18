using Eshop.Entity.Identities;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Identities
{
    public class Identity_UserClaimsConfiguration : IEntityTypeConfiguration<UserClaimEntity>
    {
        public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
        {
            builder.ToTable("UserClaim", DbSchema.Identity.ToString());
            // builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasKey(x => x.Id);


        }
    }
}
