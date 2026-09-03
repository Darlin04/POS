using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Presentation;

public static class ServiceRegistration
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        Application.ServiceRegistration.AddApplicationServices(services);
        Persistence.ServiceRegistration.AddPersistenceServices(services, configuration);

        return services;
    }
}