using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Common.Interceptor;
using Eshop.Entity.General;
using Eshop.Entity.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rasam.Data.DBContext
{
    public partial class ApplicationContext : IdentityDbContext<UserEntity, RoleEntity, Guid, UserClaimEntity, UserRoleEntity, UserLoginEntity, RoleClaimEntity, UserTokenEntity>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IConfiguration _configuration;
        public ApplicationContext(
            DbContextOptions<ApplicationContext> options,
            ICurrentUserProvider currentUserProvider,
            IConfiguration configuration) : base(options)
        {
            _currentUserProvider = currentUserProvider;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);

            //افزودن شنود تبدیل حروف "ی" و "ک" از عربی به فارسی
            optionsBuilder.AddInterceptors(new YeKeInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = typeof(IBaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<IBaseEntity>(assembly);
            modelBuilder.RegisterEntityTypeConfiguration();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var currentUserId = _currentUserProvider.UserId;
                foreach (var entity in ChangeTracker.Entries())
                {
                    if (_currentUserProvider.UserId == Guid.Empty)
                        if (!Guid.TryParse(entity.Property(nameof(IBaseEntity.CreateById)).CurrentValue.ToString(), out currentUserId) || currentUserId == Guid.Empty)
                            currentUserId = Guid.Parse("a1029412-3954-4fab-b5ec-0d102b12a94c");//tempUserId

                    switch (entity.State)
                    {
                        case EntityState.Added when entity.Entity is IBaseEntity:
                            entity.Property(nameof(IBaseEntity.CreateById)).CurrentValue = currentUserId;
                            entity.Property(nameof(IBaseEntity.CreateDate)).CurrentValue = DateTime.UtcNow;
                            entity.Property(nameof(IBaseEntity.Deleted)).CurrentValue = false;
                            break;

                        case EntityState.Modified when entity.Entity is IBaseEntity:
                            entity.Property(nameof(IBaseEntity.ModifiedById)).CurrentValue = currentUserId;
                            entity.Property(nameof(IBaseEntity.ModifiedDate)).CurrentValue = DateTime.UtcNow;
                            break;

                        case EntityState.Deleted when entity.Entity is IBaseEntity:
                            entity.Property(nameof(IBaseEntity.ModifiedById)).CurrentValue = currentUserId;
                            entity.Property(nameof(IBaseEntity.ModifiedDate)).CurrentValue = DateTime.UtcNow;
                            entity.Property(nameof(IBaseEntity.Deleted)).CurrentValue = true;
                            entity.State = EntityState.Modified;
                            break;
                    }
                }
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> PhysicalDeleteSaveChanges(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
