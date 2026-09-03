using Blocks.Domain.Abstractions;
using Blocks.EntityFramework;

namespace Inventory.Persistence.Repositories;

public class InventoryRepository<TAggregate>(InventoryDbContext dbContext)
    : GenericRepository< TAggregate, InventoryDbContext>(dbContext)
        where TAggregate : class, IAggregateRoot
{
    
}