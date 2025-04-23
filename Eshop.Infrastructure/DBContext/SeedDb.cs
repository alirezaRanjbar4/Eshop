using Eshop.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Infrastructure.DBContext
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            const string adminEmail = "admin@website.com";
            const string adminPassword = "passW0rd!";
            const string adminUsername = "admin";

            var context = serviceProvider.GetRequiredService<ApplicationContext>();
            context.Database.EnsureCreated();

            var userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            // initially create user(s)
            if (!context.Users.Any())
            {
                UserEntity user = new UserEntity()
                {
                    Email = adminEmail,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = adminUsername,
                    CreateDate = DateTime.UtcNow,
                    CreateById = Guid.Parse("B042D978-FCA7-4ECF-9E27-30F42DE4B1D5"),
                };
                var result = userManager.CreateAsync(user, adminPassword).Result;
            }

            // get the admin user we just made
            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
            if (adminUser == null)
                return;

            // make sure we have some roles
            if (!context.Roles.Any())
            {
                RoleEntity role = new RoleEntity() { Name = "Administrator", CreateDate = DateTime.Now, CreateById = Guid.Parse("B042D978-FCA7-4ECF-9E27-30F42DE4B1D5") };

                var result = roleManager.CreateAsync(role).Result;

                // assign admin role to admin
                var ur = userManager.AddToRoleAsync(adminUser, "Administrator").Result;
            }
        }
    }
}
