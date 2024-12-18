using Eshop.Entity.Identities;
using Eshop.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rasam.Data.ModelConfiguration.Identities
{
    public class Identoty_UserTokensConfiguration : IEntityTypeConfiguration<UserTokenEntity>
    {
        public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
        {
            builder.ToTable("UserToken", DbSchema.Identity.ToString());
        }
    }
}
