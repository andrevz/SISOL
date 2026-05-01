using System;
using System.Collections.Generic;
using System.Text;

namespace SISOL.Application.Common.Contracts.CQRS;

internal delegate Task<TResult> RequestHandlerDelegate<TResult>();

internal interface IPipelineBehavior<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default);
}
