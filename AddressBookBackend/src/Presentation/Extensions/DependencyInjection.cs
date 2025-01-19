using AddressBook.src.Application;
using AddressBook.src.Application.Services;
using AddressBook.src.Infrastructure.Repositories;

namespace AddressBook.src.Presentation.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(MappingProfile)); 
        }
    }
}
