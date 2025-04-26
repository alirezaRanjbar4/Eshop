using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Infrastructure.ModelConfiguration.Identities
{
    public class UserLoginsConfiguration : IEntityTypeConfiguration<UserLoginEntity>
    {
        public void Configure(EntityTypeBuilder<UserLoginEntity> builder)
        {
            builder.ToTable("UserLogin", DbSchema.Identity.ToString());
        }
    }
}
