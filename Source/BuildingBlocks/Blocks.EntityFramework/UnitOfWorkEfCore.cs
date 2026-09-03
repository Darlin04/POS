using Blocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public abstract class UnitOfWorkEfCore<TDbContext>(TDbContext dbContext) : IUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext = dbContext;

    public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default) 
        => await _dbContext.SaveChangesAsync(cancellationToken);
}