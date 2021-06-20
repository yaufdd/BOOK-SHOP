using System.Collections.Generic;
using Data.Factories;
using Data.Factories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseFactory(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionDictionary = new Dictionary<DatabaseConnectionTypes, string>
            {
                {DatabaseConnectionTypes.Default, configuration.GetConnectionString("DefaultConnection")},
            };

            services.AddSingleton<IDictionary<DatabaseConnectionTypes, string>>(connectionDictionary);
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        }
    }
}