using Core.StorageManagers;
using Core.StorageManagers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Configuration
{
    public static class StorageManagersConfiguration
    {
        public static void AddStorageManagers(this IServiceCollection services)
        {
            services.AddScoped<IBooksStorageManager, BooksStorageManager>();
        }
    }
}