using Eshop.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Eshop.Presentation.Extensions.IOC
{
    public static class AddDbContextConfiguration
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            );

            return services;
        }
    }
}
