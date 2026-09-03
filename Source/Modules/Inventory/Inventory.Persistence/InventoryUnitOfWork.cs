using Blocks.EntityFramework;

namespace Inventory.Persistence;

public class InventoryUnitOfWork(InventoryDbContext dbContext)
    : UnitOfWorkEfCore<InventoryDbContext>(dbContext);
