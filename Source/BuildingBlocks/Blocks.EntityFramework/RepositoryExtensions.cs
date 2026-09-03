using Blocks.Application.Exceptions;
using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static class RepositoryExtensions
{
    public static async Task<TEntity> FindByIdOrThrowAsync<TEntity>(
        this IGenericRepository<TEntity> repository, Guid id, string propertyName, CancellationToken cancellationToken = default)
        where TEntity : class, IAggregateRoot
    {
        var entity = await repository.FindByIdAsync(id, cancellationToken);
        return entity ?? throw new NotFoundException($"Recurso {propertyName} no encontrado");
    }
    
    public static async Task<TEntity> FindByIdOrThrowAsync<TEntity>(this DbSet<TEntity> dbSet, Guid id, string propertyName, CancellationToken cancellationToken = default)
        where TEntity : class, IAggregateRoot
    {
        var entity = await dbSet.FindAsync(id, cancellationToken);
        return entity ?? throw new NotFoundException($"Recurso {propertyName} no encontrado");
    }
    
    public static async Task<TEntity> GetByIdOrThrowAsync<TEntity>(
        this IGenericRepository<TEntity> repository, Guid id, string propertyName, CancellationToken cancellationToken = default)
        where TEntity : class, IAggregateRoot
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity ?? throw new NotFoundException($"Recurso {propertyName} no encontrado");
    }
}