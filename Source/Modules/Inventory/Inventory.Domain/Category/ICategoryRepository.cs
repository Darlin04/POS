using Blocks.Domain.Abstractions;
using Blocks.Domain.ValueObjects;

namespace Inventory.Domain.Category;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> CategoryExists(Guid categoryId);
    Task<bool> DeactivateAsync(Guid categoryId);
    Task<bool> ActivateAsync(Guid categoryId);
}