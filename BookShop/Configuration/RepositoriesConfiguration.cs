using Data.Repositories;
using Data.Repositories.DbRepositories;
using Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBooksRepository, BooksDatabaseRepository>();
        }
    }
}