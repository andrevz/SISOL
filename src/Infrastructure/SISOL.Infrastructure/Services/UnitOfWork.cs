using Microsoft.EntityFrameworkCore.Storage;
using SISOL.Application.Common.Contracts.Services.Persistence;
using SISOL.Infrastructure.Configurations.Persistence.Context;

namespace SISOL.Infrastructure.Services;

internal class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("A transaction is already in progress");

        _currentTransaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("No transaction in progress to commit.");

            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction is null)
                throw new InvalidOperationException("No transaction in progress to commit.");

            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
