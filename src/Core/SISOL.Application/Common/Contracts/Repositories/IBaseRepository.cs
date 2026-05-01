using SISOL.Domain.Entities;

namespace SISOL.Application.Common.Contracts.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<(ICollection<TResult> Collection, int TotalCount)> ListAsync<TResult, TKey>(
            Func<TEntity, TResult> selector,
            Func<TEntity, bool>? predicate = null,
            Func<TEntity, TKey>? orderBy = null,
            int page = 1,
            int pageSize = 20);
}
