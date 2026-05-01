using Microsoft.EntityFrameworkCore;
using SISOL.Application.Common.Contracts.Repositories;
using SISOL.Domain.Entities;
using SISOL.Infrastructure.Configurations.Persistence.Context;

namespace SISOL.Infrastructure.Adapters.Repositories;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _context.Set<TEntity>().AddAsync(entity);

        return result.Entity;
    }

    public async Task<(ICollection<TResult> Collection, int TotalCount)> ListAsync<TResult, TKey>(
        Func<TEntity, TResult> selector,
        Func<TEntity, bool>? predicate = null,
        Func<TEntity, TKey>? orderBy = null,
        int page = 1,
        int pageSize = 20)
    {
        var totalCount = 0;

        var query = _context.Set<TEntity>().AsQueryable();

        if (predicate != null)
            query = query.Where(predicate).AsQueryable();

        totalCount = await query.CountAsync();

        if (orderBy != null)
            query = query.OrderBy(orderBy).AsQueryable();

        var collection = query.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToList();

        return (collection, totalCount);
    }
}
