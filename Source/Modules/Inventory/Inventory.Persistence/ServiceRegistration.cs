using Blocks.Domain.Abstractions;
using Inventory.Application.Features.Products.Queries;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.Products;
using Inventory.Persistence.Queries;
using Inventory.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(connectionString, b
                => b.MigrationsAssembly(typeof(InventoryDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();
        services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductQueries, ProductQueries>();
    }
}