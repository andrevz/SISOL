using Microsoft.Extensions.DependencyInjection;
using SISOL.Application.Common.Contracts.CQRS;
using SISOL.Application.Common.Contracts.Services.CQRS;

namespace SISOL.Application.Common.Services.CQRS;

internal class Dispatcher(IServiceProvider provider) : IDispatcher
{
    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
    {
        var handler = provider.GetRequiredService<IQueryHandler<TQuery, TResult>>() 
            ?? throw new InvalidOperationException($"No handler found for query of type {typeof(TQuery).Name}");

        return await handler.HandleAsync(query, cancellationToken);
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>()
            ?? throw new InvalidOperationException($"No handler found for command of type {typeof(TCommand).Name}");

        await handler.HandleAsync(command, cancellationToken);
    }

    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand, TResult>>() 
            ?? throw new InvalidOperationException($"No handler found for command of type {typeof(TCommand).Name}");

        var behaviors = provider.GetService<IEnumerable<IPipelineBehavior<TCommand, TResult>>>()?.ToArray() 
            ?? Enumerable.Empty<IPipelineBehavior<TCommand, TResult>>();

        RequestHandlerDelegate<TResult> handlerDelegate = () => handler.HandleAsync(command, cancellationToken);

        var pipeline = behaviors.Reverse().Aggregate(handlerDelegate, (next, behavior) =>
        {
            return () => behavior.Handle(command, next, cancellationToken);
        });

        return await pipeline();
    }
}
